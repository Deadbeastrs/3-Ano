from copy import deepcopy


class SearchNode:
    def __init__(self,state,parent,depth): 
        self.state = state
        self.parent = parent
        self.depth = depth
    
    def in_parent(self,state):
        if self.state == state:
            return True

        if self.parent == None:
            return False

        return self.parent.in_parent(state)

    def __str__(self):
        return f"no({self.state}, {self.depth}, {self.parent})"
    
    def __repr__(self):
        return str(self)

# Arvores de pesquisa
class SearchTreeD2:

    # construtor
    def __init__(self,problem, strategy='depth'):
        self.problem = problem
        root = SearchNode(problem, None, 0)
        self.open_nodes = [root]
        self.strategy = strategy
        self.solution = None
        self.terminals = 1
        self.non_terminals = 0
        self.visited = []
        

    # obter o caminho (sequencia de estados) da raiz ate um no
    def get_path(self,node):
        if node.parent == None:
            return [node.state]
        path = self.get_path(node.parent)
        path += [node.state]
        return(path)

    # procurar a solucao
    def search(self, limit=None):
        result = []
        while self.open_nodes != []:
            node = self.open_nodes.pop(0)
            self.non_terminals +=1
            if node.state.isDone: #a node que estamos é a solução para o problema? (neste caso das cidades)
                result.append(node.state.state)
            lnewnodes = [] #Caso nao seja a solução
            for a in node.state.availableMoves(): #Lista de todos os caminhos possiveis que partem da node
                newstate= deepcopy(node.state)
                newstate.move(a)
                newnode = SearchNode(newstate,node,node.depth+1) # Cria uma nova node com state parent e depth
                if newnode.state.state not in self.visited: # limitar a profundidade a pesquisar e nao pode estar na lista de parents para nao andar sempre nos mesmos
                    self.visited.append(newnode.state.state)
                    lnewnodes.append(newnode) # mete na lista
            self.add_to_open(lnewnodes) #A maneira como adicionamos a nova lista de nodes nas open nodes de forma a fazer uma breadth ou depth etc
        return result

    # juntar novos nos a lista de nos abertos de acordo com a estrategia
    def add_to_open(self,lnewnodes):
        if self.strategy == 'breadth':
            self.open_nodes.extend(lnewnodes)
        elif self.strategy == 'depth':
            self.open_nodes[:0] = lnewnodes
        elif self.strategy == 'uniform':
            pass