#José Luis Costa nº92996
#encoding: utf8

from semantic_network import *
from bayes_net import *
from constraintsearch import *
from collections import Counter


class MyBN(BayesNet):

    def individual_probabilities(self):
        res = {}
        for v in self.dependencies.keys():
            variables = [k for k in self.dependencies.keys() if k != v]
            s = sum([self.jointProb([(v, True)] + conj) for conj in self._generator_conjunctions(variables)])
            res[v] = s
        return res

    def _generator_conjunctions(self, variables):
        if len(variables) == 1:
            return [[(variables[0], True)], [(variables[0], False)]]
        l = []
        for conj in self._generator_conjunctions(variables[1:]):
            l.append([(variables[0],True)] + conj)
            l.append([(variables[0],False)] + conj)
        return l


class MySemNet(SemanticNetwork):
    def __init__(self):
        SemanticNetwork.__init__(self)

    def translate_ontology(self):
        e2 = set()
        ans = []
        for i in self.query_local(relname = 'subtype'): #Retirar apenas as entidades2 diferentes (Set)
            e2.add(i.relation.entity2)
        s = ' or '
        for i in sorted(list(e2)):#Todas as entidades1 que tem a entidade 2 em subtype, que utilizando o metodo join, e umas concatenações de strings resulta numa linha
            ans.append('Qx ' + s.join(sorted(list(set([f'{a.relation.entity1.capitalize()}(x)' for a in self.query_local(relname = 'subtype') if a.relation.entity2 == i])))) + f' => {i.capitalize()}(x)')
        return ans

    def query_inherit(self,entity,assoc):
        return self._query_inherit(entity,assoc,[])

    def _query_inherit(self,cl,assoc,res):
        #Locais Normal
        query = self.query_local(e1=cl, relname=assoc)
        #Locais Invertidas
        inv = [ d for d in self.declarations if type(d.relation).__name__ == 'Association' and d.relation.inverse == assoc and d.relation.entity2 == cl]
        #Adicionar ao resultado final
        if inv != []:
            res.extend(inv)
        if query != []:
            res.extend(query)
        #Proximos predecessores a serem analisados
        nextCl = self.query_local(e1=cl, relname='subtype')
        nextCl.extend(self.query_local(e1=cl, relname='member'))
        for i in nextCl:
            self._query_inherit(i.relation.entity2,assoc,res)
        return res
        

    def query(self,entity,relname):
        #Se for subtype e member é direto
        if relname == 'subtype':
            return [a.relation.entity2 for a in self.query_local(e1=entity,relname='subtype')]
        if relname == 'member':
            return [a.relation.entity2 for a in self.query_local(e1=entity,relname='member')]
        #Retirar todos os nomes das associações não repedias
        allAssoc = set([a.relation.name for a in self.query_local() if type(a.relation).__name__ == 'Association'])
        newQuery = []
        #Retirar as associações que estão em minoria de acordo com o assoc_properties() para cada nome de associação
        for assoc in allAssoc:
            c = Counter([a.relation.assoc_properties() for a in self.query_local() if a.relation.name == assoc])
            newQuery.extend([a for a in self.query_local() if type(a.relation).__name__ == 'Association' and a.relation.name == assoc and a.relation.assoc_properties() == c.most_common(1)[0][0]])
        #Acabar de construir a nova Rede semântica já com as minorias retiradas
        newQuery.extend(self.query_local(relname = 'subtype'))
        newQuery.extend(self.query_local(relname = 'member'))
        return list(set([a.relation.entity2 for a in self._query(newQuery,entity,relname,[])]))

    def _query(self,newQuery,entity,relname,res):
        #Associações locais
        query = [a for a in newQuery if a.relation.entity1 == entity and a.relation.name == relname]
        if query != []:
            #Se encontrar um single, então verifica o que acontece mais vezes localmente e retorna imediatamente
            if query[0].relation.cardinality == 'single':
                c = Counter([a.relation for a in query])
                res.append([a for a in query if str(a.relation) == str(c.most_common(1)[0][0])][0])
                return res
            res.extend(query)
        #Proximos predecessores
        nextCl = [a for a in newQuery if a.relation.entity1 == entity and (a.relation.name == 'subtype' or a.relation.name == 'member')]
        for i in nextCl:
            self._query(newQuery,i.relation.entity2,relname,res)
        return res

class MyCS(ConstraintSearch):

    def search_all(self,domains=None,xpto=[]):
        if domains==None:
            domains = self.domains

        #Retornar uma lista de forma a conseguirmos percorrer cada possibilidade e não o dicinario em sí
        # se alguma variavel tiver lista de valores vazia, falha
        if any([lv==[] for lv in domains.values()]):
            return None

        # se nenhuma variavel tiver mais do que um valor possivel, sucesso
        if all([len(lv)==1 for lv in list(domains.values())]):
            return [{ v:lv[0] for (v,lv) in domains.items() }]
        
        # Guadar no xpto as letras que já foram set (var) em arrays diferentes
        res = []
        var = [a for a in domains.keys() if a not in xpto][0]
        xpto = xpto + [var]

        for val in domains[var]:
            newdomains = dict(domains)
            newdomains[var] = [val]
            edges = [(v1,v2) for (v1,v2) in self.constraints if v2==var]
            newdomains = self.constraint_propagation(newdomains,edges)
            solution = self.search_all(newdomains,xpto)
            if solution != None:
                for sol in solution:
                    #ir adicionando as soluções prévias e novas soluções
                    res.append(sol)
        return res
        
        


