import sqlite3


con = sqlite3.connect('dados.db', check_same_thread=False)

# creating cursor
cur = con.cursor()
cur1 = con.cursor()
cur2 = con.cursor()
cur3 = con.cursor()


def insertEstafeta(nome1,nome2,email,pwd,veiculo,tel,data,endr,codPos,cidade,pais,estado):
    t = (nome1,nome2,email,pwd,veiculo,tel,data,endr,codPos,cidade,pais,estado)
    cur.execute('SELECT * FROM `infoEstafeta` WHERE `email` LIKE ?',(email,))
    if cur.fetchone() != None:
        return False
    cur.execute("INSERT INTO `infoEstafeta` (`id`, `nome1`, `nome2`, `email`, `pwd`, `Veiculo`, `tel`, `dataNasci`, `endr`, `codPos`, `cidade`, `pais`, `estado`) VALUES (NULL, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);", t)
    if cur.rowcount == 0:
        print("Não criei nada!")
        return False
    con.commit()
    return True

def alterEstafeta(id,nome1,nome2,email,pwd,veiculo,tel,data,endr,codPos,cidade,pais,estado):
    t = (nome1,nome2,email,pwd,veiculo,tel,data,endr,codPos,cidade,pais,estado,id)
    cur.execute('SELECT * FROM `infoEstafeta` WHERE `email` LIKE ? AND `id` NOT LIKE ?',(email,id))
    if cur.fetchone() != None:
        return False
    cur.execute('UPDATE infoEstafeta SET `nome1` = ?, `nome2` = ?, `email` = ?, `pwd` = ?, `Veiculo` = ?, `tel` = ?, `dataNasci` = ?, `endr` = ?, `codPos` = ?, `cidade` = ?, `pais` = ?, `estado` = ?  WHERE id = ?', t)
    con.commit() 
    return True

def insertRestaurante(nome,email,pwd,tel,endr,codPos,cidade,pais,estado,desc,tempo,img):
    t = (nome,email,pwd,tel,endr,codPos,cidade,pais,estado,desc,tempo,img)
    cur.execute('SELECT * FROM `infoRestaurante` WHERE `email` LIKE ?',(email,))
    if cur.fetchone() != None:
        return False
    cur.execute("INSERT INTO `infoRestaurante` (`id`, `nome`, `email`, `pwd`, `tel`, `endr`, `codPos`, `cidade`, `pais`, `estado`, `descInicial`, `tempo`, `img`) VALUES (NULL, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);", t)
    if cur.rowcount == 0:
        print("Não criei nada!")
        return False
    con.commit()
    return True

def alterRestaurante(id,nome,email,pwd,tel,endr,codPos,cidade,pais,estado,desc,tempo):
    t = (nome,email,pwd,tel,endr,codPos,cidade,pais,estado,desc,tempo,id)
    cur.execute('SELECT * FROM `infoRestaurante` WHERE `email` LIKE ? AND `id` NOT LIKE ?',(email,id))
    if cur.fetchone() != None:
        return False
    cur.execute('UPDATE infoRestaurante SET `nome` = ?, `email` = ?, `pwd` = ?, `tel` = ?, `endr` = ?, `codPos` = ?, `cidade` = ?, `pais` = ?, `estado` = ?, `descInicial` = ?, `tempo` = ?  WHERE id = ?', t)
    con.commit() 
    return True

def insertCliente(nome1,nome2,email,pwd,tel,data):
    t = (nome1,nome2,email,pwd,tel,data)
    cur.execute('SELECT * FROM `infoCliente` WHERE `email` LIKE ?',(email,))
    if cur.fetchone() != None:
        return False
    cur.execute("INSERT INTO `infoCliente` (`id`, `nome1`, `nome2`, `email`, `pwd`, `tel`, `dataNasci`) VALUES (NULL, ?, ?, ?, ?, ?, ?);", t)
    if cur.rowcount == 0:
        print("Não criei nada!")
        return False
    con.commit()
    return True

def alterCliente(id,nome1,nome2,email,pwd,tel,data):
    t = (nome1,nome2,email,pwd,tel,data,id)
    cur.execute('SELECT * FROM `infoCliente` WHERE `email` LIKE ? AND `id` NOT LIKE ?',(email,id))
    if cur.fetchone() != None:
        return False
    cur.execute('UPDATE infoCliente SET `nome1` = ?, `nome2` = ?, `email` = ?, `pwd` = ?, `tel` = ?, `dataNasci` = ?  WHERE id = ?', t)
    con.commit() 
    return True

def alterMorada(id,nome,codPos,cidade,pais,estado,endereco):
    t = (nome,codPos,cidade,pais,estado,endereco,id)
    cur.execute('SELECT * FROM `morada` WHERE `nome` LIKE ? AND `id` NOT LIKE ?',(nome,id))
    if cur.fetchone() != None:
        return False
    cur.execute('UPDATE morada SET `nome` = ?, `codPostal` = ?, `cidade` = ?, `pais` = ?, `estado` = ?, `endereco` = ?  WHERE id = ?', t)
    con.commit()
    return True

def alterCartao(id,nome,numero,validade,ccv):
    t = (nome,numero,validade,ccv,id)
    cur.execute('SELECT * FROM `cartao` WHERE `nome` LIKE ? AND `id` NOT LIKE ?',(nome,id))
    if cur.fetchone() != None:
        return False
    cur.execute('UPDATE cartao SET `nome` = ?, `numero` = ?, `validade` = ?, `ccv` = ?  WHERE id = ?', t)
    con.commit()
    return True

def insertMorada(idCliente,nome,codPos,cidade,pais,estado,endereco):
    t = (idCliente,nome,codPos,cidade,pais,estado,endereco)
    cur.execute("INSERT INTO `morada` (`id`, `idCliente`, `nome`, `codPostal`, `cidade`, `pais`, `estado`, `endereco`) VALUES (NULL, ?, ?, ?, ?, ?, ?, ?);", t)
    if cur.rowcount == 0:
        print("Não criei nada!")
        return False
    con.commit()
    return True

def insertCartao(idCliente,nome,numero,validade,ccv):
    t = (idCliente,nome,numero,validade,ccv)
    cur.execute("INSERT INTO `cartao` (`id`, `idCliente`, `nome`, `numero`, `validade`, `ccv`) VALUES (NULL, ?, ?, ?, ?, ?);", t)
    if cur.rowcount == 0:
        print("Não criei nada!")
        return False
    con.commit()
    return True

def insertPrato(idRestaurante,cat,nome,preco,desc,info,img):
    t = (idRestaurante,cat,nome,preco,desc,info,img)
    cur.execute("INSERT INTO `prato` (`id`, `idRest`, `categoria`, `nome`, `preco`, `descricao`, `infoNutri`, `img`) VALUES (NULL, ?, ?, ?, ?, ?, ?, ?);", t)
    if cur.rowcount == 0:
        print("Não criei nada!")
        return False
    con.commit()
    return True

def insertPedido(idRest,idEst,idCli,pedido):
    print(f"{idRest},{idEst},{idCli},{pedido}")
    t = (int(idRest),int(idEst),int(idCli),pedido,0,0,'n')
    cur.execute("INSERT INTO `pedidosPend` (`id`, `idRest`, `idEst`, `idCli`, `pedido`, `idCartao`, `idMorada`, `isReady`) VALUES (NULL, ?, ?, ?, ?, ?, ?, ?);", t)
    if cur.rowcount == 0:
        print("Não criei nada!")
        return False
    con.commit()
    return True

def searchForClient(email,pwd):
    t = (email,pwd)
    cur.execute('SELECT * FROM `infoCliente` WHERE `email` LIKE ? AND `pwd` LIKE ?', t)
    return cur.fetchone()

def searchForRestaurante(email,pwd):
    t = (email,pwd)
    cur.execute('SELECT * FROM `infoRestaurante` WHERE `email` LIKE ? AND `pwd` LIKE ?', t)
    return cur.fetchone()

def searchForEstafeta(email,pwd):
    t = (email,pwd)
    cur.execute('SELECT * FROM `infoEstafeta` WHERE `email` LIKE ? AND `pwd` LIKE ?', t)
    return cur.fetchone()

def getClientInfo(id):
    t = (id,)
    cur.execute('SELECT * FROM `infoCliente` WHERE `id` LIKE ?', t)
    info = cur.fetchone()
    return {'nome1':info[1],'nome2':info[2],'email':info[3],'pwd':info[4],'tel':info[5],'data':info[6]}

def getEstafetaInfo(id):
    t = (id,)
    cur.execute('SELECT * FROM `infoEstafeta` WHERE `id` LIKE ?', t)
    info = cur.fetchone()
    return {'nome1':info[1],'nome2':info[2],'email':info[3],'pwd':info[4],'veiculo':info[5],'tel':info[6],'data':info[7],'endr':info[8],'codPos':info[9],'cidade':info[10],'pais':info[11],'estado':info[12]}

def getAllRestauratsInfo():
    cur.execute('SELECT * FROM `infoRestaurante`')
    idList = [a[0] for a in cur.fetchall()]
    ans = []
    for id_ in idList:
        info = getRestauranteInfo(id_)
        ans.append((id_,info['nome'],info['tempo']+" min",info['desc'],"short_" + info['img']))
    return ans

def getRestauranteInfo(id):
    t = (id,)
    cur.execute('SELECT * FROM `infoRestaurante` WHERE `id` LIKE ?', t)
    info = cur.fetchone()
    return {'id':info[0],'nome':info[1],'email':info[2],'pwd':info[3],'tel':info[4],'endr':info[5],'codPos':info[6],'cidade':info[7],'pais':info[8],'estado':info[9],'desc':info[10],'tempo':info[11],'img':info[12]}

def getRestaurantPlates(id):
    t = (id,)
    cur2.execute('SELECT * FROM `prato` WHERE `idRest` LIKE ?', t)
    return cur2.fetchall()

def getCartaoForClient(id):
    t = (id,)
    cur1.execute('SELECT * FROM `cartao` WHERE `idCliente` LIKE ?', t)
    return cur1.fetchall()

def getAddressForClient(id):
    t = (id,)
    cur1.execute('SELECT * FROM `morada` WHERE `idCliente` LIKE ?', t)
    return cur1.fetchall()

def getRestIdByName(name):
    t = (name,)
    cur1.execute('SELECT * FROM `infoRestaurante` WHERE `nome` LIKE ?', t)
    return cur1.fetchone()

def getRandomEstafetaId():
    cur1.execute('SELECT * FROM infoEstafeta ORDER BY RANDOM() LIMIT 1')
    res = cur1.fetchone()
    if res == None:
        return 0
    return res[0]

def getRestPed(id):
    t = (id,)
    cur3.execute('SELECT * FROM `pedidosPend` WHERE `idRest` LIKE ? AND `isReady` LIKE "n";', t)
    res = cur3.fetchall()
    if res == None:
        return []
    return res

def getEstPed(id):
    t = (id,)
    cur3.execute('SELECT * FROM `pedidosPend` WHERE `idEst` LIKE ? AND `isReady` LIKE "y";', t)
    res = cur3.fetchall()
    if res == None:
        return []
    return res

def sendOrderConfirm(id):
    t = (id,)
    cur3.execute('UPDATE `pedidosPend` SET `isReady` = "y" WHERE `pedidosPend`.`id` = ?;', t)
    con.commit()
    return True

def removeRestaurantPlate(idPrato,idRest):
    t = (idPrato,idRest)
    cur1.execute('DELETE FROM `prato` WHERE `id` LIKE ? AND `idRest` LIKE ?', t)
    con.commit()
    return True

def removePedido12(idPedido):
    t = (idPedido)
    cur1.execute('DELETE FROM `pedidosPend` WHERE `id` LIKE ?', t)
    con.commit()
    return True

def showAllClientes():
    cur.execute('SELECT * FROM `infoCliente`')
    return cur.fetchall()

def showAllRestaurantes():
    cur.execute('SELECT * FROM `infoRestaurante`')
    return cur.fetchall()

def showAllEstafetas():
    cur.execute('SELECT * FROM `infoEstafeta`')
    return cur.fetchall()

def showAllMoradas():
    cur.execute('SELECT * FROM `morada`')
    return cur.fetchall()

def showAllPratos():
    cur.execute('SELECT * FROM `prato`')
    return cur.fetchall()

def showAllCartoes():
    cur.execute('SELECT * FROM `cartao`')
    return cur.fetchall()

def showAllPedidos():
    cur3.execute('SELECT * FROM `pedidosPend`')
    return cur3.fetchall()

def clearAll():
    cur.execute('DELETE FROM morada')
    cur.execute('DELETE FROM cartao')
    cur.execute('DELETE FROM infoCliente')
    cur.execute('DELETE FROM infoRestaurante')
    cur.execute('DELETE FROM infoEstafeta')
    cur.execute('DELETE FROM prato')
    cur.execute('DELETE FROM pedidosPend')
    con.commit()
