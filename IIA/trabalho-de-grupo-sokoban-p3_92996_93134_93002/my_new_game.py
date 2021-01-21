#José Luís Costa 92996
#Diogo Mendonça 93002
#Guilherme Pereira 93134

from copy import deepcopy
import json
from typing import DefaultDict, Set
from my_new_mapa import Map, Tiles
import math
from tree_searchKeeper import *
import sys

class BoxGame:

    def __init__(self, level, deadPos):
        self.level = level
        self._lastkeypress = ""
        self.map = Map(level)
        self._state = {
            "boxes": self.map.boxes
        }
        self.deathSquaresList = deadPos
        self.testetime = 0
        self.testetime1 = 0

    def setOnMap(self,cur,npos,ctile):
        self.map.set_tile(npos, ctile)
        self.map.clear_tile(cur)

    def moving(self, cur, direction, isTest):
        """Move an entity in the game."""
        assert direction in "wasd", f"Can't move in {direction} direction"

        cx, cy = cur
        ctile = self.map.get_tile(cur)

        npos = cur
        if direction == "w":
            npos = cx, cy - 1
        if direction == "a":
            npos = cx - 1, cy
        if direction == "s":
            npos = cx, cy + 1
        if direction == "d":
            npos = cx + 1, cy

        # test blocked
        if self.map.is_blocked(npos):
            return False
        
        if not isTest:
            self.setOnMap(cur,npos,ctile)
        return True

    def move(self,box,key):
        self._lastkeypress = key
        val = self.moving(box, self._lastkeypress,False)
        self._state = {
            "boxes": self.map.boxes
        }
        return val
    
    def movingKeeper(self, cur, direction, isTest):
        """Move an entity in the game."""
        assert direction in "wasd", f"Can't move in {direction} direction"

        cx, cy = cur
        ctile = self.map.get_tile(cur)

        npos = cur
        if direction == "w":
            npos = cx, cy - 1
        if direction == "a":
            npos = cx - 1, cy
        if direction == "s":
            npos = cx, cy + 1
        if direction == "d":
            npos = cx + 1, cy

        # test blocked
        if self.map.is_blocked(npos):
            return False
        if self.map.get_tile(npos) in [
            Tiles.BOX,
            Tiles.BOX_ON_GOAL,
        ]:  # next position has a box?
            if ctile & Tiles.MAN == Tiles.MAN:  # if you are the keeper you can push
                if not self.movingKeeper(npos, direction,isTest):  # as long as the pushed box can move
                    return False
            else:  # you are not the Keeper, so no pushing
                return False
        if not isTest:
            self.setOnMap(cur,npos,ctile)
        return True

    def moveKeeper(self,key):
        self._lastkeypress = key
        val = self.movingKeeper(self.map.keeper, self._lastkeypress,False)
        return val
    
    @property
    def state(self):
        """Contains the state of the Game."""
        return json.dumps(self._state)
    
    @property
    def isDone(self):
        return self.map.completed

    def doesKeeperReach(self,box,kmoves,direction):
        cx, cy = box
        if direction == "w":
            npos = cx, cy - 1
        if direction == "a":
            npos = cx - 1, cy
        if direction == "s":
            npos = cx, cy + 1
        if direction == "d":
            npos = cx + 1, cy

        if npos not in kmoves:
            return False

        return True
    
    def isDeadlockBox(self,box,direction,walls,goals,boxes):
        cx, cy = box
        if direction == "w":
            npos = cx, cy - 1
        if direction == "a":
            npos = cx - 1, cy
        if direction == "s":
            npos = cx, cy + 1
        if direction == "d":
            npos = cx + 1, cy
        
        ctile = self.map.get_tile(npos)
        if not (ctile == Tiles.FLOOR or ctile == Tiles.MAN or ctile == Tiles.MAN_ON_GOAL or ctile == Tiles.GOAL):
            return False

        
        info = (set([a for a in boxes]),goals,walls)
        
        
        info[0].remove(box)
        info[0].add(npos)

        
        if self.deadlockDetection(info):
            return False
        return True
    

    def availableMoves(self):
        self.time1 = 0
        search = SearchTreeK(self.map)
        kmoves = search.search()
        moves = []
        walls = set(self.map.filter_tiles([Tiles.WALL]))
        goals = set(self.map.filter_tiles([Tiles.GOAL,Tiles.MAN_ON_GOAL,Tiles.BOX_ON_GOAL]))
        boxes = set(self.map.filter_tiles([Tiles.BOX,Tiles.BOX_ON_GOAL]))
        for box in self.map.boxes:
            flag = False
            info = [box,[],self.getDistance(self.map.keeper,box)]
            if self.doesKeeperReach(box,kmoves,'w'):
                flag = True
                if self.isDeadlockBox(box,'s',walls,goals,boxes):
                    info[1].append('s')
            if self.doesKeeperReach(box,kmoves,'s'):
                flag = True
                if self.isDeadlockBox(box,'w',walls,goals,boxes):
                    info[1].append('w')
            if self.doesKeeperReach(box,kmoves,'d'):
                flag = True
                if self.isDeadlockBox(box,'a',walls,goals,boxes):
                    info[1].append('a')
            if self.doesKeeperReach(box,kmoves,'a'):
                flag = True
                if self.isDeadlockBox(box,'d',walls,goals,boxes):
                    info[1].append('d')
            if flag == True:
                moves.append(info)
        moves.sort(key=lambda x: x[2])
        return moves

    def getDistance(self,keeper,box):
        xBox, yBox = box
        xKeeper, yKeeper = keeper
        return math.hypot(xKeeper-xBox,yKeeper-yBox)


    def moveBox(self, box, direction):
        cx,cy = box
        if direction == "w":
            npos = cx, cy + 1
        if direction == "a":
            npos = cx + 1, cy
        if direction == "s":
            npos = cx, cy - 1
        if direction == "d":
            npos = cx - 1, cy
        return npos

    def moveBox1(self, box, direction):
        cx,cy = box
        if direction == "w":
            npos = cx, cy - 1
        if direction == "a":
            npos = cx - 1, cy
        if direction == "s":
            npos = cx, cy + 1
        if direction == "d":
            npos = cx + 1, cy
        return npos
    
    def deadlockDetection(self,info):
        for numb in self.deathSquaresList:
            self.deathSquaresList[numb]['cur'] = 0
            for boxes in info[0]:
                if boxes in self.deathSquaresList[numb]['floorTiles']:
                    self.deathSquaresList[numb]['cur'] += 1
                if self.deathSquaresList[numb]['max'] < self.deathSquaresList[numb]['cur']:
                    return True
        
        if(self.freezeDeadlocks(info)):
            return True
        return False
        
    def freezeDeadlocks(self,info):
        return len([box for box in info[0] if self.freezeDeadlocksRec(self.map,box,"XY",set(),info)]) != 0
            

    def freezeDeadlocksRec(self,map,box,axis,visited,info):
        #Horizontal
        blkX = False
        blkY = False
        #PAREDE
        if axis == "XY":
            visited.add((box[0],box[1]))
            if box in info[1]:
                return False
            if (box[0]-1,box[1]) in info[2] or (box[0]+1,box[1]) in info[2] :
                blkX = True
            if (box[0],box[1]-1) in info[2] or (box[0],box[1]+1) in info[2] :
                blkY = True
            if (box[0]-1,box[1]) in self.deathSquaresList['vazio']['floorTiles'] and (box[0]+1,box[1]) in self.deathSquaresList['vazio']['floorTiles'] :
                blkX = True
            if (box[0],box[1]-1) in self.deathSquaresList['vazio']['floorTiles'] and (box[0],box[1]+1) in self.deathSquaresList['vazio']['floorTiles'] :
                blkY = True
            if (box[0]-1,box[1]) in info[0] and self.freezeDeadlocksRec(map,(box[0],box[1]),'Y',visited,info):
                blkX = self.freezeDeadlocksRec(map,(box[0]-1,box[1]),'Y',visited,info)
            if (box[0]+1,box[1]) in info[0] and self.freezeDeadlocksRec(map,(box[0],box[1]),'Y',visited,info):
                blkX = self.freezeDeadlocksRec(map,(box[0]+1,box[1]),'Y',visited,info)
            if (box[0],box[1]-1) in info[0] and self.freezeDeadlocksRec(map,(box[0],box[1]),'X',visited,info):
                blkY = self.freezeDeadlocksRec(map,(box[0],box[1]-1),'X',visited,info)
            if (box[0],box[1]+1) in info[0] and self.freezeDeadlocksRec(map,(box[0],box[1]),'X',visited,info):
                blkY = self.freezeDeadlocksRec(map,(box[0],box[1]+1),'X',visited,info)
            
            if blkX and blkY:
                return True
            return False

        elif axis == "X":
            if box in visited:
                return True    
            visited.add((box[0],box[1]))
            if (box[0]-1,box[1]) in info[2] or (box[0]+1,box[1]) in info[2] :
                return True
            if (box[0]-1,box[1]) in self.deathSquaresList['vazio']['floorTiles'] and (box[0]+1,box[1]) in self.deathSquaresList['vazio']['floorTiles'] :
                return True
            if (box[0]-1,box[1]) in info[0]:
                return self.freezeDeadlocksRec(map,(box[0]-1,box[1]),'Y',visited,info)
            if (box[0]+1,box[1]) in info[0]:
                return self.freezeDeadlocksRec(map,(box[0]+1,box[1]),'Y',visited,info)
            return False
            
        elif axis == "Y":
            if box in visited:
                return True    
            visited.add((box[0],box[1]))
            if (box[0],box[1]-1) in info[2] or (box[0],box[1]+1) in info[2] :
                return True
            if (box[0],box[1]-1) in self.deathSquaresList['vazio']['floorTiles'] and (box[0],box[1]+1) in self.deathSquaresList['vazio']['floorTiles'] :
                return True
            if (box[0],box[1]-1) in info[0]:
                return self.freezeDeadlocksRec(map,(box[0],box[1]-1),'X',visited,info)
            if (box[0],box[1]+1) in info[0]:
                return self.freezeDeadlocksRec(map,(box[0],box[1]+1),'X',visited,info)
            return False
            
        if blkX and blkY:
            return False

class Deadlock:
    def __init__(self, level):
        self.level = level
        self._lastkeypress = ""
        self.map = Map(level)
        self.resetBoard()

    def setBox(self,boxPos):
        self.map.set_tile(boxPos, Tiles.BOX)
        self.boxPos = boxPos
        self._state = {
            "box": boxPos
        }

    def resetBoard(self):
        nonGoalPos = self.map.filter_tiles([Tiles.BOX, Tiles.BOX_ON_GOAL, Tiles.MAN])
        clearGoals = self.map.filter_tiles([Tiles.BOX_ON_GOAL, Tiles.MAN_ON_GOAL])
        for pos in nonGoalPos:
            self.map.set_tile(pos, Tiles.FLOOR)
            self.map.clear_tile(pos)
        
        for pos in clearGoals:
            self.map.set_tile(pos, Tiles.GOAL)
            self.map.clear_tile(pos)
        
    def getFloorTiles(self):
        return self.map.filter_tiles([Tiles.FLOOR])

    def moving(self, cur, direction, isTest):
        """Move an entity in the game."""
        assert direction in "wasd", f"Can't move in {direction} direction"
        canMoveX = True
        canMoveY = True
        cx, cy = cur
        npos = cur
        if cx > 0 and cy > 0 and cx+1 < self.map.hor_tiles  and cy+1 < self.map.ver_tiles :
            if self.map.get_tile((cx-1,cy)) == Tiles.WALL or self.map.get_tile((cx+1,cy)) == Tiles.WALL :
                canMoveX = False

            if self.map.get_tile((cx,cy-1)) == Tiles.WALL or self.map.get_tile((cx,cy+1)) == Tiles.WALL:
                canMoveY = False

            if direction == "w" and canMoveY:
                npos = cx, cy - 1
            if direction == "a" and canMoveX:
                npos = cx - 1, cy
            if direction == "s" and canMoveY:
                npos = cx, cy + 1
            if direction == "d" and canMoveX:
                npos = cx + 1, cy

            if npos == cur:
                return False

            # test blocked
            if self.map.is_blocked(npos):
                return False
            if isTest:
                self.boxPos = npos
                self._state = {
                    "box": self.boxPos
                }
                self._lastkeypress = direction
                self.map.set_tile(npos, Tiles.BOX)
                self.map.clear_tile(cur)
            
            return True
    
    def move(self,dir):
        return self.moving(self.boxPos,dir,True)

    def isMoveAvailable(self,pos,dir):
        return self.moving(pos,dir,False)

    def availableMoves(self):
        moves = []
        if self.isMoveAvailable(self.boxPos,'w'):
            moves.append('w')
        if self.isMoveAvailable(self.boxPos,'a'):
            moves.append('a')
        if self.isMoveAvailable(self.boxPos,'s'):
            moves.append('s')
        if self.isMoveAvailable(self.boxPos,'d'):
            moves.append('d')
        return moves

    @property
    def state(self):
        """Contains the state of the Game."""
        return self._state
    
    @property
    def isDone(self):
        return self.map.filter_tiles([Tiles.BOX]) == []


class keeperGame:
    def __init__(self, map , goal):
        self._lastkeypress = ""
        self.map = map
        self._state = {
            "keeper": self.map.keeper,
        }
        self.goal = goal
    
    def setOnMap(self,cur,npos,ctile):
        self.map.set_tile(npos, ctile)
        self.map.clear_tile(cur)

    def moving(self, cur, direction, isTest):
        """Move an entity in the game."""
        assert direction in "wasd", f"Can't move in {direction} direction"

        cx, cy = cur
        ctile = self.map.get_tile(cur)

        npos = cur
        if direction == "w":
            npos = cx, cy - 1
        if direction == "a":
            npos = cx - 1, cy
        if direction == "s":
            npos = cx, cy + 1
        if direction == "d":
            npos = cx + 1, cy

        # test blocked
        if self.map.is_blocked(npos):
            return False
        if self.map.get_tile(npos) in [
            Tiles.BOX,
            Tiles.BOX_ON_GOAL,
        ]:  # next position has a box?
            if ctile & Tiles.MAN == Tiles.MAN:  # if you are the keeper you can push
                if not self.moving(npos, direction,isTest):  # as long as the pushed box can move
                    return False
            else:  # you are not the Keeper, so no pushing
                return False
        if not isTest:
            self.setOnMap(cur,npos,ctile)
        return True

    def move(self,key):
        self._lastkeypress = key
        val = self.moving(self.map.keeper, self._lastkeypress,False)
        self._state = {
            "keeper": self.map.keeper,
        }
        return val

    @property
    def state(self):
        """Contains the state of the Game."""
        return json.dumps(self._state)
    
    @property
    def isDone(self):
        return self.map.keeper == self.goal

    def isMoveAvailable(self,keeper,mov):
        cx, cy = keeper
        if mov == "w":
            keeper = cx, cy - 1
        if mov == "a":
            keeper = cx - 1, cy
        if mov == "s":
            keeper = cx, cy + 1
        if mov == "d":
            keeper = cx + 1, cy
        
        if self.map.get_tile(keeper) == Tiles.FLOOR or self.map.get_tile(keeper) == Tiles.GOAL:
            return True

        return False

    def availableMoves(self):
        moves = []
        if self.isMoveAvailable(self.map.keeper,'w'):
            moves.append('w')
        if self.isMoveAvailable(self.map.keeper,'a'):
            moves.append('a')
        if self.isMoveAvailable(self.map.keeper,'s'):
            moves.append('s')
        if self.isMoveAvailable(self.map.keeper,'d'):
            moves.append('d')
        return moves
    
    def heuristicKeeper(self):
        xGoal = self.goal[0]
        yGoal = self.goal[1]
        xKeeper = self.map.keeper[0]
        yKeeper = self.map.keeper[1]
        heuristica = math.hypot(xGoal-xKeeper,yGoal-yKeeper)
        return heuristica
