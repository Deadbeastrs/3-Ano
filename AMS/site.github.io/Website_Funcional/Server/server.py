from flask import Flask, render_template, request, redirect, url_for, session, make_response
from flask_cors import CORS
import sqlite3
import re
import datetime
from utilsDB import *
import jwt
import json
from PIL import Image, ImageFile
from imageConf import _Image
import time
import pathlib

app = Flask(__name__)
app.secret_key = 'teste123'
CORS(app)
JWT_SECRET_KEY = 't1NP63m4wnBg6nyHYKfmc2TpCOGI4nss'
ImageFile.LOAD_TRUNCATED_IMAGES = True

@app.route('/login', methods=['POST'])
def login():
    if request.method == 'POST' and 'email' in request.form and 'pwd' in request.form and 'type' in request.form:
        email = request.form['email']
        pwd = request.form['pwd']
        typeOf = request.form['type']
        if typeOf == 'Cliente':
            info = searchForClient(email,pwd)
        elif typeOf == 'Estafeta':
            info = searchForEstafeta(email,pwd)
        elif typeOf == 'Restaurante':
            info = searchForRestaurante(email,pwd)
        if info != None:
            payload = {
                'exp': datetime.datetime.utcnow() + datetime.timedelta(days=0,minutes=5),
                'iat': datetime.datetime.utcnow(),
                'sub': info[0],
                'type' : typeOf
            }
            # Redirect to home page
            token = generate_jwt_token(payload)
            resp = make_response(token)
            resp.headers.add('Access-Control-Allow-Credentials','true')
            return resp
        else:
            # Account doesnt exist or username/password incorrect
            resp = make_response("Erro!",404)
            resp.headers.add('Access-Control-Allow-Credentials','true')
            return resp


@app.route('/getInfo',methods=['GET'])
def profile():
    tipo = request.args['type']
    status,info = checkToken(request,tipo)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    print(searchInfo(tipo,info))
    resp = make_response(json.dumps(searchInfo(tipo,info)))
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/getCardsAndAddr',methods=['GET'])
def cardsAndAddr():
    status,info = checkToken(request,"Cliente")
    #debug
    #clearAll()
    #print(getAllRestauratsInfo())
    #print(showAllRestaurantes())
    #print(getRestaurantPlates(1))
    print(status)
    #print(showAllEstafetas())
    #print(showAllClientes())
    #print(showAllMoradas())
    #print(showAllCartoes())
    #------
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    ans = (getAddressForClient(info),getCartaoForClient(info))
    resp = make_response(json.dumps(ans))
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/getSpecInfo',methods=['GET'])
def getInfo():
    tipo = request.args['type']
    id = request.args['id']
    status,info = checkToken(request,'Cliente','Restaurante')
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    resp = make_response(json.dumps(searchInfo(tipo,id)))
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/getRestaurantPlates',methods=['GET'])
def getInfoPlates():
    id = request.args['id']
    status,info = checkToken(request,'Cliente','Restaurante')
    print(status)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    resp = make_response(json.dumps(getRestaurantPlates(id)))
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/getAllRestaurants',methods=['GET'])
def restList():
    tipo = "Cliente"
    status,info = checkToken(request,tipo)
    #debug
    #clearAll()
    #print(getAllRestauratsInfo())
    print(showAllPratos())
    #print(showAllClientes())
    #print(showAllMoradas())
    #print(showAllCartoes())
    #------
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    resp = make_response(json.dumps(getAllRestauratsInfo()))
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/getPedidos',methods=['GET'])
def getPedidos():
    tipo = request.args['type']
    status,infoId = checkToken(request,tipo)
    print(tipo)
    print(status)
    print(infoId)
    print(showAllPedidos())
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    if tipo == 'Restaurante':
        resp = make_response(json.dumps(getRestPed(infoId)))
    if tipo == 'Estafeta':
        print(getEstPed(infoId))
        resp = make_response(json.dumps(getEstPed(infoId)))
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp


@app.route('/newPrato',methods=['POST'])
def addPrato():
    tipo = "Restaurante"
    status,infoId = checkToken(request,tipo)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    info = json.loads(request.form['info'])
    insertPrato(infoId,"-",info['nome'],info['preco'],info['desc'],info['infoN'],info['img'])
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/insertMorada',methods=['POST'])
def addMorada():
    tipo = "Cliente"
    status,infoId = checkToken(request,tipo)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    info = json.loads(request.form['info'])
    print(info)
    insertMorada(infoId,info['nome'],info['codPos'],info['cidade'],info['pais'],info['estado'],info['endr'])
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/insertCartao',methods=['POST'])
def addCartao():
    tipo = "Cliente"
    status,infoId = checkToken(request,tipo)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    info = json.loads(request.form['info'])
    print(info)
    insertCartao(infoId,info['nome'],info['numero'],info['validade'],info['ccv'])
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/removePrato',methods=['POST'])
def removePrato():
    tipo = "Restaurante"
    idprato = request.form['id']
    status,infoId = checkToken(request,tipo)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    removeRestaurantPlate(idprato,infoId)
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/removePedido',methods=['POST'])
def removePedido():
    tipo = "Estafeta"
    idpedido = request.form['id']
    status,infoId = checkToken(request,tipo)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    removePedido12(idpedido)
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/confirmOrder',methods=['POST'])
def confirmPrato():
    tipo = "Restaurante"
    idpedido = request.form['id']
    status,infoId = checkToken(request,tipo)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    sendOrderConfirm(idpedido)
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/modifyRest',methods=['POST'])
def modifyRest():
    tipo = "Restaurante"
    info = json.loads(request.form['info'])
    status,infoId = checkToken(request,tipo)
    print(status)
    if not status:
        print("AKI")
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    if not alterRestaurante(infoId,info['nome'],info['email'],info['pwd'],info['tel'],info['endr'],info['codPos'],info['cidade'],info['pais'],info['estado'],info['desc'],info['tempo']): #inserir restaurante
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/modifyCliente',methods=['POST'])
def modifyCliente():
    tipo = "Cliente"
    print("teste")
    info = json.loads(request.form['info'])
    status,infoId = checkToken(request,tipo)
    print(info)
    if not status:
        print("DSJAKDjsa")
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    alterCliente(infoId,info['nome1'],info['nome2'],info['email'],info['pwd'],info['tel'],info['data'])
    alterMorada(info['idMorada'],info['nomeMorada'],info['codPosMorada'],info['cidadeMorada'],info['paisMorada'],info['estadoMorada'],info['endrMorada'])
    alterCartao(info['idCartao'],info['nomeCartao'],info['numeroCartao'],info['validadeCartao'],info['ccvCartao'])
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/modifyEst',methods=['POST'])
def modifyEst():
    tipo = "Estafeta"
    info = json.loads(request.form['info'])
    status,infoId = checkToken(request,tipo)
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    if not alterEstafeta(infoId,info['nome1'],info['nome2'],info['email'],info['pwd'],info['veiculo'],info['tel'],info['data'],info['endr'],info['codPos'],info['cidade'],info['pais'],info['estado']):
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp

@app.route('/sendPedido',methods=['POST'])
def sendPedido():
    tipo = "Cliente"
    print("AKI")
    rest = request.form['rest']
    pedido = request.form['info']
    status,infoId = checkToken(request,tipo)
    print(showAllEstafetas())
    if not status:
        resp = make_response("Erro!",404)
        resp.headers.add('Access-Control-Allow-Credentials','true')
        return resp
    
    insertPedido(getRestIdByName(rest)[0],getRandomEstafetaId(),infoId,pedido)
    resp = make_response("Sucesso!")
    resp.headers.add('Access-Control-Allow-Credentials','true')
    return resp


@app.route('/uploadImage',methods=['GET','POST'])
def upload():
    #isthisFile = request.files['image']
    print("ESTOUAKI ")
    print("dksladksaldkalwsdksaldksaldkasldksal")
    isthisFile=request.files['file']
    print("olaaaa")
    print(isthisFile)
    print("akideu")
    print("-/static/img/" + str(isthisFile.filename))
    isthisFile.save("./static/img/"+isthisFile.filename)
    isthisFile.close()
    print("PASEEI")
    cutImage("./static/img/"+isthisFile.filename,"./static/img/long_"+isthisFile.filename,1200,288)
    cutImage("./static/img/"+isthisFile.filename,"./static/img/short_"+isthisFile.filename,1300,1300)
    cutImage("./static/img/"+isthisFile.filename,"./static/img/food_"+isthisFile.filename,154,123)
    return "Worked"

@app.route('/register',methods=['POST'])
def register():
    if request.method == 'POST' and 'info' in request.form and 'type' in request.form:
        info = json.loads(request.form['info'])
        typeOf = request.form['type']
        status1 = False
        status2 = False
        status3 = False
        #Cliente
        if typeOf == 'Cliente':
            print("123211-1")
            status1 = insertCliente(info['nome1'],info['nome2'],info['email'],info['pwd'],info['tel'],info['data']) #Inserir Cliente
            print(status1)
            if status1:
                clientId = searchForClient(info['email'],info['pwd']) #Buscar id do cliente
                status2 = insertMorada(clientId[0],info['nomeEndr'],info['codPos'],info['cidade'],info['pais'],info['estado'],info['endr']) #Inserir Morada
                status3 = insertCartao(clientId[0],info['nomeCartao'],info['numeroCartao'],info['validadeCartao'],info['ccvCartao']) #Inserir Cartao
        #Estafeta
        elif typeOf == 'Estafeta':
            print("123211-2")
            status1 = insertEstafeta(info['nome1'],info['nome2'],info['email'],info['pwd'],info['veiculo'],info['tel'],info['data'],info['endr'],info['codPos'],info['cidade'],info['pais'],info['estado']) #inserir estafeta
            print("AKI->>>>> " + str(status1))
            status2 = True
            status3 = True
            clientId = searchForEstafeta(info['email'],info['pwd']) #Buscar id do estafeta
        #Restaurante
        elif typeOf == 'Restaurante':
            print("DJSKDJSKDJSK")
            status1 = insertRestaurante(info['nome1'],info['email'],info['pwd'],info['tel'],info['endr'],info['codPos'],info['cidade'],info['pais'],info['estado'],info['desc'],info['tempo'],info['img']) #inserir restaurante
            print("AKI->>>>> " + str(status1))
            status2 = True
            status3 = True
            clientId = searchForRestaurante(info['email'],info['pwd']) #Buscar id do restaurante
        print("fim")
        if status1 and status2 and status3:
            payload = {
                'exp': datetime.datetime.utcnow() + datetime.timedelta(days=0,minutes=20),
                'iat': datetime.datetime.utcnow(),
                'sub': clientId[0],
                'type' : typeOf
            }
            # Redirect to home page
            token = generate_jwt_token(payload)
            resp = make_response(token)
            resp.headers.add('Access-Control-Allow-Credentials','true')
            return resp
        else:
            # Account doesnt exist or username/password incorrect
            resp = make_response("Erro!",404)
            resp.headers.add('Access-Control-Allow-Credentials','true')
            return resp



def checkToken(request,typeOf,typeOfAlt = None):
    if 'token' in request.cookies:
        info = decode_jwt_token(request.cookies.get('token'))
        if info == "Expired" or info == "Invalid":
            return (False,'')
        if type(info['sub']) == int and (info['type'] == typeOf or info['type'] == typeOfAlt):
            return (True,info['sub'])
        return (False,'')
    return (False,'')

#com o user id
def generate_jwt_token(content):
    encoded_content = jwt.encode(content, JWT_SECRET_KEY, algorithm="HS256")
    token = str(encoded_content).split("'")[0]
    return token

def decode_jwt_token(auth_token):
    try:
        payload = jwt.decode(auth_token, JWT_SECRET_KEY, algorithms=["HS256"])
        return payload
    except jwt.ExpiredSignatureError:
        return 'Expired'
    except jwt.InvalidTokenError:
        return 'Invalid'

def searchInfo(typeOf,info):
    if typeOf == 'Cliente':
        return getClientInfo(info)
    elif typeOf == 'Estafeta':
        return getEstafetaInfo(info)
    elif typeOf == 'Restaurante':
        return getRestauranteInfo(info)
    
def cutImage(img,destPath,width,height):
    imgConf = _Image(img)
    img = imgConf.crop_to_aspect(width,height)
    #imgConf.img.show()
    img.save(destPath)

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8080)
