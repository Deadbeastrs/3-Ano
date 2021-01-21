#José Luís Costa 92996
#Diogo Mendonça 93002
#Guilherme Pereira 93134


from abc import ABC, abstractmethod
from copy import deepcopy
import time
import asyncio
from consts import *
import math

class SearchNode:
    def __init__(self,state,parent,move): 
        self.state = state
        self.parent = parent
        self.move = move
    def in_parent(self,state):
        if self.state == state:
            return True

        if self.parent == None:
            return False

        return self.parent.in_parent(state)

    def __str__(self):
        return f"no({self.state} [{self.move}])"
    
    def __repr__(self):
        return str(self)
        
    def __lt__(self,other):
        return 1



# Arvores de pesquisa
class SearchTreeKpath:

    # construtor
    def __init__(self,map,keeper,boxes,goal):
        info = keeper
        root = SearchNode(info,None, '')
        self.map = map
        self.wall = set(map.filter_tiles([Tiles.WALL]) + boxes)
        self.open_nodes = [root]
        self.solution = None
        self.visited = set()
        self.timeDead = 0
        self.sates = 0
        self.timeStates = 0
        self.goal = goal

    def get_path(self,node):
        if node.parent == None:
            return [node]
        path = self.get_path(node.parent)
        path += [node]
        return(path)

    # procurar a solucao
    async def search(self):
        await asyncio.sleep(0)
        self.teste1 = 0
        while len(self.open_nodes) != 0:
            node = self.open_nodes.pop(0)
            if node.state == self.goal:
                return self.get_path(node)
            lnewnodes = []
            for a in self.getAvailablePos(node): #Lista de todos os caminhos possiveis que partem da node
                newnode = SearchNode(a[0],node,a[1]) # Cria uma nova node com state parent e depth
                if newnode.state not in self.visited : # limitar a profundidade a pesquisar e nao pode estar na lista de parents para nao andar sempre nos mesmos 
                    self.visited.add(newnode.state)
                    lnewnodes.append(newnode)
            self.open_nodes.extend(lnewnodes)
        return []
    
    def getAvailablePos(self,node):
        available = []
        cx, cy = node.state
        npos = cx, cy - 1
        if npos not in self.wall:
            available.append((npos,'w'))
        npos = cx - 1, cy
        if npos not in self.wall:
            available.append((npos,'a'))
        npos = cx, cy + 1
        if npos not in self.wall:
            available.append((npos,'s'))
        npos = cx + 1, cy
        if npos not in self.wall:
            available.append((npos,'d'))
        return available
