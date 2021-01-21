from constraintsearch import *

region = ['A', 'B', 'C', 'D', 'E']
colors = ['red', 'blue', 'green', 'yellow', 'white']

dominio = {
    'A' : colors,
    'B' : colors,
    'C' : colors,
    'D' : colors,
    'E' : colors
}

mapa = {
    'A': ['B','E','D'],
    'B': ['A','E','C'],
    'C': ['B','E','D'],
    'D': ['A','E','C'],
    'E': ['A','B','C','D']
}

def make_constraint_graph():
    return { (X,Y):map_constraint for X in region for Y in region if X!=Y }

def map_constraint(r1,c1,r2,c2):
    if r2 in mapa[r1]:
        if c1 == c2:
            return False
    return True

cs = ConstraintSearch(dominio, make_constraint_graph())

def g(x):
    if x == []:
        return [[]]
    y = g(x[1:])
    return y + [[x[0]]+z for z in y]

print(g([1,2,3,4,5]))

#print(cs.search())
