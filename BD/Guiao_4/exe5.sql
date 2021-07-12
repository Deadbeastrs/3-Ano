-- Pessoa

CREATE TABLE PESSSOA (
    Email           VARCHAR(60)     NOT NULL,
    Nome            VARCHAR(60)     NOT NULL,
    Morada          VARCHAR(100)    NOT NULL,
    PRIMARY KEY (Email)
);

-- Conferencia

CREATE TABLE CONFERENCIA (
    Local           VARCHAR(60)     NOT NULL,
    Nome            VARCHAR(60)     NOT NULL,
    PRIMARY KEY (Local)
);

-- Participantes

CREATE TABLE PARTICIPANTES (
    Email_Pessoa        VARCHAR(60)     NOT NULL,
    Data_Inscricao      VARCHAR(15)     NOT NULL,
    Local_Conf          VARCHAR(60)    NOT NULL,
    PRIMARY KEY (Email_Pessoa),
    FOREIGN KEY (Email_Pessoa) REFERENCES PESSSOA(Email),
    FOREIGN KEY (Local_Conf) REFERENCES CONFERENCIA(Local)
);

-- Não Estudante

CREATE TABLE NAO_ESTUDANTE (
    Email_Pessoa_Part   VARCHAR(60)     NOT NULL,
    Ref_Trans           VARCHAR(60)     NOT NULL,
    PRIMARY KEY (Email_Pessoa_Part),
    FOREIGN KEY (Email_Pessoa_Part) REFERENCES PARTICIPANTES(Email_Pessoa)
);

-- Instituicao

CREATE TABLE INSTITUICAO (
    Endereco   VARCHAR(60)     NOT NULL,
    Nome       VARCHAR(60)     NOT NULL,
    PRIMARY KEY (Endereco)
);

-- Comprovativo

CREATE TABLE COMPROVATIVO (
    Identificador   VARCHAR(60)     NOT NULL,
    Endr_inst_emi   VARCHAR(60)     NOT NULL,
    Localizacao     VARCHAR(100)    NOT NULL,
    PRIMARY KEY (Identificador),
    FOREIGN KEY (Endr_inst_emi) REFERENCES INSTITUICAO(Endereco)
);

-- Estudante

CREATE TABLE ESTUDANTE (
    Email_Pessoa_Part   VARCHAR(60)     NOT NULL,
    Ident_compr         VARCHAR(60)     NOT NULL,
    PRIMARY KEY (Email_Pessoa_Part),
    FOREIGN KEY (Email_Pessoa_Part) REFERENCES PARTICIPANTES(Email_Pessoa),
    FOREIGN KEY (Ident_compr) REFERENCES COMPROVATIVO(Identificador)
);

-- Artigo Cientifico

CREATE TABLE ARTIGO_C (
    Numero              INT             NOT NULL,
    Titulo              VARCHAR(60)     NOT NULL,
    Local_conf          VARCHAR(60)    NOT NULL,
    PRIMARY KEY (Numero),
    FOREIGN KEY (Local_conf) REFERENCES CONFERENCIA(Local)
);

-- Autor

CREATE TABLE AUTOR (
    Email_pessoa    VARCHAR(60)     NOT NULL,
    Endereco_inst   VARCHAR(60)     NOT NULL,
    PRIMARY KEY (Email_pessoa),
    FOREIGN KEY (Endereco_inst) REFERENCES INSTITUICAO(Endereco)
);

-- TEM

CREATE TABLE TEM (
    Email_autor         VARCHAR(60)     NOT NULL,
    Numero_artigo       INT             NOT NULL,
    PRIMARY KEY (Email_autor),
    FOREIGN KEY (Email_autor) REFERENCES AUTOR(Email_pessoa),
    FOREIGN KEY (Numero_artigo) REFERENCES ARTIGO_C(Numero)
);