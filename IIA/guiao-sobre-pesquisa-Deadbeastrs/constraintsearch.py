# Pesquisa para resolucao de problemas de atribuicao
# 
# Introducao a Inteligencia Artificial
# DETI / UA
#
# (c) Luis Seabra Lopes, 2012-2019
#


class ConstraintSearch:

    # domains é um dicionário com o domínio de cada variável;
    # constaints e' um dicionário com a restrição aplicável a cada aresta;
    def __init__(self,domains,constraints):
        self.domains = domains
        self.constraints = constraints
        self.calls = 0

    # domains é um dicionário com os domínios actuais
    # de cada variável
    # ( ver acetato "Pesquisa com propagacao de restricoes
    #   em problemas de atribuicao - algoritmo" )
    def search(self,domains=None):
        print("starting")
        self.calls += 1 
        
        if domains==None:
            domains = self.domains

        # se alguma variavel tiver lista de valores vazia, falha
        if any([lv==[] for lv in domains.values()]):
            return None

        # se nenhuma variavel tiver mais do que um valor possivel, sucesso
        if all([len(lv)==1 for lv in list(domains.values())]):
            return { v:lv[0] for (v,lv) in domains.items() }
       
        # continuação da pesquisa
        # ( falta fazer a propagacao de restricoes )
        for var in domains.keys():
            if len(domains[var])>1:
                for val in domains[var]:
                    newdomains = dict(domains)
                    if val[0] == val[1]:
                        continue
                    newdomains[var] = [val]
                    print(newdomains)
                    newdomains = self.constrain(newdomains,var,val)
                    print(newdomains)
                    solution = self.search(newdomains)
                    if solution != None:
                        return solution
        return None

    def constrain(self,domain,var1,val1):
        newdomain = {}
        for var in domain.keys():
            if var1 != var:
                temp = [] + domain[var]
                for val in domain[var]:
                    constraint = self.constraints[var1,var]
                    if not constraint(var1,val1,var,val):
                        temp.remove(val)
                domain[var] = temp
        return domain
