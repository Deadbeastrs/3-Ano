#José Luís Costa 92996
#Diogo Mendonça 93002
#Guilherme Pereira 93134


from abc import ABC, abstractmethod
from copy import deepcopy
from tree_searchKeeper import SearchTreeK
from my_new_game import keeperGame
import time
import asyncio
from consts import *
import math
from queue import PriorityQueue

class MyPriorityQueue(PriorityQueue):
    def __init__(self):
        PriorityQueue.__init__(self)
        self.counter = 0

    def put(self, item, priority):
        PriorityQueue.put(self, (priority, self.counter, item))
        self.counter += 1

    def get(self, *args, **kwargs):
        _, _, item = PriorityQueue.get(self, *args, **kwargs)
        return item

class SearchNode:
    def __init__(self,state,parent,move,cost,heur): 
        self.state = state
        self.parent = parent
        self.move = move
        self.cost = cost
        self.heur = heur

    def __str__(self):
        return f"{self.state}"
    
    def __repr__(self):
        return str(self)

# Arvores de pesquisa
class SearchTreeBoxAS:

    # construtor
    def __init__(self,game): 
        root = SearchNode(game.map, None, '',0,1)
        root.state.set_tile(root.state.keeper,Tiles.FLOOR)
        self.game = game
        self.open_nodes = MyPriorityQueue()
        self.open_nodes.put(root,root.heur + root.cost)
        self.keeperStart = game.map.keeper
        self.visited = set()
        self.deathSquareList = game.deathSquaresList
        self.walls = []
        self.goals = game.map.filter_tiles([Tiles.GOAL,Tiles.BOX_ON_GOAL,Tiles.MAN_ON_GOAL])
        for i in range(game.map.size[0]):
            self.walls.append([])
            for j in range(game.map.size[1]):
                self.walls[i].append(game.map.get_tile((i,j)) == Tiles.WALL)


    # obter o caminho (sequencia de estados) da raiz ate um no
    def get_path(self,node):
        if node.parent == None:
            return [node]
        path = self.get_path(node.parent)
        path += [node]
        return(path)

    # procurar a solucao
    async def search(self):
        while not self.open_nodes.empty():
            await asyncio.sleep(0)
            node = self.open_nodes.get()
            if node.state.completed: #a node que estamos é a solução para o problema? (neste caso das cidades)
                #print(f"TimeKeeper -> {self.timeKeeper}, AVmovesTime ->{self.sates} teste1->{teste1} avail->{teste12342}, deep->{goalRoom}, wallMacro->{wallMacro},tunnel->{tun}\n")
                return self.get_path(node) #Retornamos o caminho para a node
            lnewnodes = [] #Caso nao seja a solução
            self.game.map = node.state
            avMoves = self.game.availableMoves()
            for box in avMoves: #Lista de todos os caminhos possiveis que partem da node
                for mov in box[1]:
                    isTunnel = False
                    self.game.map = deepcopy(node.state)
                    keeperbew = self.game.map.keeper
                    self.game.map.clear_tile(self.game.map.keeper) 
                    tunelList = []
                    tempbox = box[0]
                    for i in range(self.tunel(self.game,mov,tempbox)-2):
                        isTunnel = True
                        self.game.move(tempbox,mov)
                        tempbox = self.game.moveBox1(tempbox,mov)
                        tunelList.append(mov)

                    for i in range(self.wallMacro(self.game.map,tempbox,mov)-2):
                        self.game.move(tempbox,mov)
                        tempbox = self.game.moveBox1(tempbox,mov)
                        tunelList.append(mov)
                    
                    if ((self.walls[tempbox[0]][tempbox[1]+1] and self.walls[tempbox[0]][tempbox[1]-1]) or(self.walls[tempbox[0]+1][tempbox[1]] and self.walls[tempbox[0]-1][tempbox[1]])): 
                        if (self.room(self.game.map,tempbox,mov,keeperbew)):
                            if not (self.walls[self.game.moveBox1(tempbox,mov)[0]][self.game.moveBox1(tempbox,mov)[1]] or self.game.moveBox1(tempbox,mov) in self.game.map.boxes):
                                tempbox2 = self.game.moveBox1(tempbox,mov)
                                if not (self.walls[self.game.moveBox1(tempbox2,mov)[0]][self.game.moveBox1(tempbox2,mov)[1]] or self.game.moveBox1(tempbox2,mov) in self.game.map.boxes):
                                    if isTunnel :
                                        self.game.move(tempbox,mov)
                                        tempbox = self.game.moveBox1(tempbox,mov)
                                        tunelList.append(mov)
                                    self.game.move(tempbox,mov)
                                    tempbox = self.game.moveBox1(tempbox,mov)
                                    self.game.move(tempbox,mov)
                                    self.game.map.set_tile(tempbox,Tiles.MAN)
                                    tempbox = self.game.moveBox1(tempbox,mov)
                                    newnode = SearchNode(self.game.map,node,[self.game.moveBox(box[0],mov),tunelList + [mov]+[mov],self.game.map.keeper,tempbox,box[0]],0,self.getHeur(self.game.map,tempbox)) # Cria uma nova node com state parent e depth
                                    info = f"{newnode.state.boxes, newnode.state.keeper}"   
                                    if info not in self.visited: # limitar a profundidade a pesquisar e nao pode estar na lista de parents para nao andar sempre nos mesmos
                                        self.visited.add(info)
                                        lnewnodes.append((newnode,newnode.heur+newnode.cost))
                                    continue
                            continue
                    self.game.move(tempbox,mov)
                    self.game.map.set_tile(tempbox,Tiles.MAN)
                    tempbox = self.game.moveBox1(tempbox,mov)
                    newnode = SearchNode(self.game.map,node,[self.game.moveBox(box[0],mov),tunelList+[mov],self.game.map.keeper,tempbox,box[0]],0,self.getHeur(self.game.map,tempbox)) # Cria uma nova node com state parent e depth
                    info = f"{newnode.state.boxes, newnode.state.keeper}"   
                    if info not in self.visited: # limitar a profundidade a pesquisar e nao pode estar na lista de parents para nao andar sempre nos mesmos
                        self.visited.add(info)
                        lnewnodes.append((newnode,newnode.heur+newnode.cost))
            for i in range(len(lnewnodes)):
                self.open_nodes.put(lnewnodes[i][0],lnewnodes[i][1])
        return []

    def tunel(self,game,mov,box):
        tunelLength = 0
        if mov == 'd':
            if (self.walls[box[0]][box[1]+1] and not self.walls[box[0]][box[1]-1]) or (self.walls[box[0]][box[1]-1] and not self.walls[box[0]][box[1]+1]):
                if game.map.get_tile((box[0] + 1,box[1])) == Tiles.FLOOR:
                    tunelLength += 1
                    box = (box[0] + 1,box[1])
                else:
                    return tunelLength
            while(self.walls[box[0]][box[1]+1] and self.walls[box[0]][box[1]-1]):
                if game.map.get_tile((box[0] + 1,box[1])) == Tiles.FLOOR:
                    tunelLength += 1
                else:
                    return tunelLength
                box = (box[0] + 1,box[1])
            return tunelLength
        if mov == 'a':
            if (self.walls[box[0]][box[1]+1] and not self.walls[box[0]][box[1]-1]) or (self.walls[box[0]][box[1]-1] and not self.walls[box[0]][box[1]+1]):
                if game.map.get_tile((box[0] -1,box[1])) == Tiles.FLOOR:
                    tunelLength += 1
                    box = (box[0]-1,box[1])
                else:
                    return tunelLength
            while(self.walls[box[0]][box[1]+1] and self.walls[box[0]][box[1]-1]):
                if game.map.get_tile((box[0] -1 ,box[1])) == Tiles.FLOOR:
                    tunelLength += 1
                else:
                    return tunelLength
                box = (box[0] - 1,box[1])
            return tunelLength
        if mov == 'w':
            if (self.walls[box[0]+1][box[1]] and not self.walls[box[0]-1][box[1]]) or (self.walls[box[0]-1][box[1]] and not self.walls[box[0]+1][box[1]]):
                if game.map.get_tile((box[0],box[1]-1)) == Tiles.FLOOR:
                    tunelLength += 1
                    box = (box[0],box[1]-1)
                else:
                    return tunelLength
            while(self.walls[box[0]+1][box[1]] and self.walls[box[0]-1][box[1]]):
                if game.map.get_tile((box[0],box[1]-1)) == Tiles.FLOOR:
                    tunelLength += 1
                else:
                    return tunelLength
                box = (box[0],box[1] - 1)
            return tunelLength
        if mov == 's':
            if (self.walls[box[0]+1][box[1]] and not self.walls[box[0]-1][box[1]] or (self.walls[box[0]-1][box[1]] and not self.walls[box[0]+1][box[1]])):
                if game.map.get_tile((box[0],box[1]+1)) == Tiles.FLOOR:
                    tunelLength += 1
                    box = (box[0],box[1]+1)
                else:
                    return tunelLength
            while(self.walls[box[0]+1][box[1]] and self.walls[box[0]-1][box[1]]):
                if game.map.get_tile((box[0],box[1]+1)) == Tiles.FLOOR:
                    tunelLength += 1
                else:
                    return tunelLength
                box = (box[0],box[1] + 1)
            return tunelLength

    def getHeur(self,map,box):
        temp = []
        xBox, yBox = box
        for goal in self.goals:
            xGoal, yGoal = goal
            temp.append(math.hypot(xGoal-xBox,yGoal-yBox))
        return min(temp)

    def getDist(self,keeper,box):
        xBox, yBox = box
        xKeeper, yKeeper = keeper
        return math.hypot(xKeeper-xBox,yKeeper-yBox)

    def room(self,map1,box,mov,keeper):
        map2 = deepcopy(map1)
        map2.set_tile(keeper,Tiles.MAN)
        lista = map2.boxes
        roomEntry = box
        for i in lista:
            map2.clear_tile(i)
            if i == box:
                map2.set_tile(box,Tiles.WALL)
        if mov == 'a':
            roomEntry = (box[0]-1,box[1])
            search = SearchTreeK(map2)
            kmoves = search.search()
            if roomEntry in kmoves:
                return False
            return True

        if mov == 's':
            roomEntry = (box[0],box[1]+1)
            search = SearchTreeK(map2)
            kmoves = search.search()
            if roomEntry in kmoves:
                return False
            return True
        if mov == 'd':
            roomEntry = (box[0]+1,box[1])
            search = SearchTreeK(map2)
            kmoves = search.search()

            if roomEntry in kmoves:
                return False
            return True

        if mov == 'w':
            roomEntry = (box[0],box[1]-1)
            search = SearchTreeK(map2)
            kmoves = search.search()
            if roomEntry in kmoves:
                return False
            return True
    
    def wallMacro(self,map2,box,mov):
        tunelLength=0
        list1 = {Tiles.WALL,Tiles.BOX,Tiles.BOX_ON_GOAL}
        if mov == 'w':
            if (self.walls[box[0]+1][box[1]] and not self.walls[box[0]-1][box[1]]):
                while(self.walls[box[0]+1][box[1]] and not self.walls[box[0]-1][box[1]]):
                    if map2.get_tile((box[0],box[1]-1)) == Tiles.FLOOR and map2.get_tile((box[0] - 1,box[1]-1)) not in list1:
                        tunelLength += 1   
                    else:
                        return tunelLength
                    box = (box[0],box[1]-1) 
                return tunelLength
            elif (self.walls[box[0]-1][box[1]] and not self.walls[box[0]+1][box[1]]):
                while(self.walls[box[0]-1][box[1]] and not self.walls[box[0]+1][box[1]]):
                    if map2.get_tile((box[0] ,box[1]-1)) == Tiles.FLOOR and map2.get_tile((box[0] + 1,box[1]-1)) not in list1:
                        tunelLength += 1
                    else:
                        return tunelLength
                    box = (box[0],box[1]-1) 
                return tunelLength
            return 0
        if mov == 'a':
            if (self.walls[box[0]][box[1]-1] and not self.walls[box[0]][box[1]+1]):
                while(self.walls[box[0]][box[1]-1] and not self.walls[box[0]][box[1]+1]):
                    if map2.get_tile((box[0]-1,box[1])) == Tiles.FLOOR and map2.get_tile((box[0] - 1,box[1]+1)) not in list1:
                        tunelLength += 1               
                    else:
                        return tunelLength
                    box = (box[0]-1,box[1]) 
                return tunelLength
            elif (self.walls[box[0]][box[1]+1] and not self.walls[box[0]][box[1]-1]):
                while(self.walls[box[0]][box[1]+1] and not self.walls[box[0]][box[1]-1]):
                    if map2.get_tile((box[0] -1,box[1])) == Tiles.FLOOR and map2.get_tile((box[0] - 1,box[1]-1)) not in list1:
                        tunelLength += 1
                    else:
                        return tunelLength
                    box = (box[0]-1,box[1]) 
                return tunelLength
            return 0
        if mov == 's':
            if (self.walls[box[0]+1][box[1]] and not self.walls[box[0]-1][box[1]]):
                while(self.walls[box[0]+1][box[1]] and not self.walls[box[0]-1][box[1]]):
                    if map2.get_tile((box[0],box[1]+1)) == Tiles.FLOOR and map2.get_tile((box[0] - 1,box[1]+1)) not in list1:
                        tunelLength += 1               
                    else:
                        return tunelLength
                    box = (box[0],box[1]+1) 
                return tunelLength
            elif (self.walls[box[0]-1][box[1]] and not self.walls[box[0]+1][box[1]]):
                while(self.walls[box[0]-1][box[1]] and not self.walls[box[0]+1][box[1]]):
                    if map2.get_tile((box[0] ,box[1]+1)) == Tiles.FLOOR and map2.get_tile((box[0] + 1,box[1]+1)) not in list1:
                        tunelLength += 1
                    else:
                        return tunelLength
                    box = (box[0],box[1]+1) 
                return tunelLength
            return 0
        if mov == 'd':
            if (self.walls[box[0]][box[1]-1] and not self.walls[box[0]][box[1]+1]):
                while(self.walls[box[0]][box[1]-1] and not self.walls[box[0]][box[1]+1]):
                    if map2.get_tile((box[0] + 1,box[1])) == Tiles.FLOOR and map2.get_tile((box[0] + 1,box[1]+1)) not in list1:
                        tunelLength += 1               
                    else:
                        return tunelLength
                    box = (box[0]+1,box[1]) 
                return tunelLength
            elif (self.walls[box[0]][box[1]+1] and not self.walls[box[0]][box[1]-1]):
                while(self.walls[box[0]][box[1]+1] and not self.walls[box[0]][box[1]-1]):
                    if map2.get_tile((box[0] + 1,box[1])) == Tiles.FLOOR and map2.get_tile((box[0] + 1,box[1]-1)) not in list1:
                        tunelLength += 1
                    else:
                        return tunelLength
                    box = (box[0]+1,box[1]) 
                return tunelLength
            return 0