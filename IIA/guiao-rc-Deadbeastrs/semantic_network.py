from collections import Counter

# Guiao de representacao do conhecimento
# -- Redes semanticas
# 
# Inteligencia Artificial & Introducao a Inteligencia Artificial
# DETI / UA
#
# (c) Luis Seabra Lopes, 2012-2020
# v1.9 - 2019/10/20
#


# Classe Relation, com as seguintes classes derivadas:
#	 - Association - uma associacao generica entre duas entidades
#	 - Subtype	 - uma relacao de subtipo entre dois tipos
#	 - Member	  - uma relacao de pertenca de uma instancia a um tipo
#

class Relation:
	def __init__(self,e1,rel,e2):
		self.entity1 = e1
#	   self.relation = rel  # obsoleto
		self.name = rel
		self.entity2 = e2
	def __str__(self):
		return self.name + "(" + str(self.entity1) + "," + \
			   str(self.entity2) + ")"
	def __repr__(self):
		return str(self)


# Subclasse Association
class Association(Relation):
	def __init__(self,e1,assoc,e2):
		Relation.__init__(self,e1,assoc,e2)

class AssocOne(Relation):
	def __init__(self,e1,assoc,e2):
		Relation.__init__(self,e1,assoc,e2)

class AssocNum(Relation):
	def __init__(self,e1,assoc,e2):
		Relation.__init__(self,e1,assoc,float(e2))


#   Exemplo:
#   a = Association('socrates','professor','filosofia')

# Subclasse Subtype
class Subtype(Relation):
	def __init__(self,sub,super):
		Relation.__init__(self,sub,"subtype",super)


#   Exemplo:
#   s = Subtype('homem','mamifero')

# Subclasse Member
class Member(Relation):
	def __init__(self,obj,type):
		Relation.__init__(self,obj,"member",type)

#   Exemplo:
#   m = Member('socrates','homem')

# classe Declaration
# -- associa um utilizador a uma relacao por si inserida
#	na rede semantica
#
class Declaration:
	def __init__(self,user,rel):
		self.user = user
		self.relation = rel
	def __str__(self):
		return "decl("+str(self.user)+","+str(self.relation)+")"
	def __repr__(self):
		return str(self)

#   Exemplos:
#   da = Declaration('descartes',a)
#   ds = Declaration('darwin',s)
#   dm = Declaration('descartes',m)

# classe SemanticNetwork
# -- composta por um conjunto de declaracoes
#	armazenado na forma de uma lista
#
class SemanticNetwork:
	def __init__(self,ldecl=None):
		self.declarations = [] if ldecl==None else ldecl
	def __str__(self):
		return my_list2string(self.declarations)
	def insert(self,decl):
		self.declarations.append(decl)
	def query_local(self,user=None,e1=None,rel=None,e2=None, relation_type=None):
		self.query_result = \
			[ d for d in self.declarations
				if  (user == None or d.user==user)
				and (e1 == None or d.relation.entity1 == e1)
				and (rel == None or d.relation.name == rel)
				and (e2 == None or d.relation.entity2 == e2) 
				and (relation_type == None or isinstance(d.relation, relation_type))]
		return self.query_result
	def show_query_result(self):
		for d in self.query_result:
			print(str(d))

	def list_associations(self):
		assoc = []
		for d in self.declarations:
			if isinstance(d.relation, Association) and d.relation.name not in assoc:
				assoc.append(d.relation.name)

		return assoc

#		return list(set([d.relation.name for d in self.declarations if isinstance(d.relation, Association)]))

	def list_objects(self):
		return list(set([d.relation.entity1 for d in self.declarations if isinstance(d.relation, Member)]))

	def list_users(self):
		return list(set([d.user for d in self.declarations]))

	def list_types(self):
		return list(set([d.relation.entity2 for d in self.declarations if isinstance(d.relation, Subtype) or isinstance(d.relation, Member)] + [d.relation.entity1 for d in self.declarations if isinstance(d.relation, Subtype)]))

	def list_local_associations(self, entity):
#		return list(set([d.relation.name for d in self.query_local(e1=entity, relation_type=Association) + self.query_local(e2=entity, relation_type=Association)]))

		return list(set( [d.relation.name for d in self.declarations if isinstance(d.relation, Association) and entity in [d.relation.entity1, d.relation.entity2]] ))

	def list_relations_by_user(self, user):
		
		return list(set( [d.relation.name for d in self.declarations if d.user == user ] ))

	def associations_by_user(self, user):
		
		return len(set([d.relation.name for d in self.declarations if isinstance(d.relation, Association) and d.user == user]))

	def list_local_associations_by_user(self, entity):
		
		return list(set([ (d.relation.name, d.user) for d in self.declarations if isinstance(d.relation, Association) and entity in [d.relation.entity1, d.relation.entity2]] )) 

	def predecessor(self, A, B):
		predec = [d.relation.entity2 for d in self.declarations if (isinstance(d.relation, Member) or isinstance(d.relation, Subtype)) and d.relation.entity1 == B]

		if A in predec:
			return True

		return any([self.predecessor(A, p) for p in predec])

	def predecessor_path(self, A, B):
		predec = [d.relation.entity2 for d in self.declarations if (isinstance(d.relation, Member) or isinstance(d.relation, Subtype)) and d.relation.entity1 == B]
		
		if A in predec:
			return [A, B]

		for p in [self.predecessor_path(A, p) for p in predec]:
			if not p is None:
				return p + [B]

		# Se predec for vazio:
		return None

	def query(self, entity, assoc_name=None):
		predec = [self.query(d.relation.entity2, assoc_name) for d in self.declarations if isinstance(d.relation, (Member, Subtype)) and d.relation.entity1 == entity]

		return [p for sublist in predec for p in sublist] + self.query_local(e1=entity, rel=assoc_name, relation_type=Association) 		

	def query2(self, entity, assoc_name=None):
		
		return self.query(entity, assoc_name) + self.query_local(e1=entity, relation_type=(Member, Subtype))

	def query_cancel(self, entity, assoc_name=None):
		predec = [self.query_cancel(d.relation.entity2, assoc_name) for d in self.declarations if isinstance(d.relation, (Member, Subtype)) and d.relation.entity1 == entity]

		local = self.query_local(e1=entity, rel=assoc_name, relation_type=Association)
		
		filtered_predec = [d for sublist in predec for d in sublist if d.relation.name not in [l.relation.name for l in local]]

		return filtered_predec + local	

	def query_down(self, type_, assoc_name, first_call = True):
		desc = [self.query_down(d.relation.entity1, assoc_name, first_call=False) for d in self.declarations if isinstance(d.relation, (Member, Subtype)) and d.relation.entity2 == type_]

		return [d for sublist in desc for d in sublist] + ([] if first_call else self.query_local(e1=type_, rel=assoc_name))
		
	def query_induce(self, type_, assoc_name):
		
		desc = self.query_down(type_, assoc_name)

		c = Counter([d.relation.entity2 for d in desc])

		for value, _ in c.most_common(1):
			return value

		return None 

	def query_local_assoc(self, entity, assoc_name):
		local_decl = self.query_local(e1 = entity, rel = assoc_name)
	
		for local in local_decl:
			if isinstance(local.relation, Association):
				c = Counter([d.relation.entity2 for d in local_decl])
				l = []
				f = 0
				for value, count in c.most_common(): 
					l.append((value, count/len(local_decl)))
					f+=count/len(local_decl)
					if f > 0.75:
						return l	
			if isinstance(local.relation, AssocOne):
				c = Counter([d.relation.entity2 for d in local_decl])
				for value, count in c.most_common(1):
					return value, count/len(local_decl)
			if isinstance(local.relation, AssocNum):
				return sum([d.relation.entity2 for d in local_decl])/len(local_decl)
		return None

	def query_assoc_value(self, E, A):
		local_decl = self.query_local(e1 = E, rel=A)

		if len(set([l.relation.entity2 for l in local_decl])) == 1:
			return local_decl[0].relation.entity2

		assoc_decl = self.query(E, A)
		inherited_decl = [a for a in assoc_decl if a not in local_decl]

		def percentagem(lista, v): #L(E,A,V) e H(E,A,V)
			if len(lista) == 0:
				return 0
			return len([l for l in lista if l.relation.entity2 == v]) / len(lista)
		
		m = max(assoc_decl, key= lambda d: (percentagem(local_decl, d.relation.entity2) + percentagem(inherited_decl, d.relation.entity2)) /2)
		if m:
			return m.relation.entity2


# Funcao auxiliar para converter para cadeias de caracteres
# listas cujos elementos sejam convertiveis para
# cadeias de caracteres
def my_list2string(list):
   if list == []:
	   return "[]"
   s = "[ " + str(list[0])
   for i in range(1,len(list)):
	   s += ", " + str(list[i])
   return s + " ]"
	

