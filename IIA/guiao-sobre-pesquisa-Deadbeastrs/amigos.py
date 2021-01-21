from constraintsearch import *

amigos = ["Andre", "Bernardo", "Claudio"]

def amigos_constraint(a1,bc1,a2,bc2):
    b1,c1 = bc1
    b2,c2 = bc2

    #Nao existem clones
    if b1 == b2 or c1 == c2:
        return False
    
    #Ninguem leva a sua
    if a1 == b1 or a1 == c1 or a2 == b2 or a2 == c2:
        return False

    #tem chapeu diferente da bicicleta
    if b1 == c1 or b2 == c2:
        return False

    #o que leva o chapeu de claudio anda na bicicleta de bernardo
    if (c1 == "Claudio" and b1 != "Bernardo") or (c2 == "Claudio" and b2 != "Bernardo"):
        return False

    return True


def make_constraint_graph(amigos_constraint):
    return { (X,Y): amigos_constraint for X in amigos for Y in amigos if X != Y}

def make_domains(amigos):
    
    return { a : [(b,c) for b in amigos for c in amigos] for a in amigos}



cs = ConstraintSearch(make_domains(amigos),make_constraint_graph(amigos_constraint))

print(cs.search())