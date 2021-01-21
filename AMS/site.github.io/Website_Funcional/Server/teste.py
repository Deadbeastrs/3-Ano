import jwt
JWT_SECRET_KEY = 't1NP63m4wnBg6nyHYKfmc2TpCOGI4nss'

token = generate_jwt_token("teste123")

print(decode_jwt_token(token))

def generate_jwt_token(content):
    encoded_content = jwt.encode(content, JWT_SECRET_KEY, algorithm="HS256")
    token = str(encoded_content).split("'")[1]
    return token

def decode_jwt_token(auth_token):
    try:
        payload = jwt.decode(auth_token, JWT_SECRET_KEY)
        return payload
    except jwt.ExpiredSignatureError:
        return 'Expired'
    except jwt.InvalidTokenError:
        return 'Invalid'