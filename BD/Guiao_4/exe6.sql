-- Pessoa

CREATE TABLE PESSSOA (
    Numero_cc       INT             NOT NULL,
    Nome            VARCHAR(60)     NOT NULL,
    Morada          VARCHAR(100)    NOT NULL,
    Data_nasc       VARCHAR(40)     NOT NULL,
    PRIMARY KEY (Numero_cc)
);

-- Adulto

CREATE TABLE ADULTO (
    Numero_cc_pessoa    INT             NOT NULL,
    Email               VARCHAR(60),
    Contacto_tel        INT,
    Relacao             VARCHAR(40)     NOT NULL,
    PRIMARY KEY (Numero_cc_pessoa),
    FOREIGN KEY (Numero_cc_pessoa) REFERENCES PESSSOA(Numero_cc)
);

-- Classe

CREATE TABLE CLASSE (
    Classe_ident    INT     NOT NULL,
    PRIMARY KEY (Classe_ident)
);

-- Ano

CREATE TABLE ANO (
    Ano_classe_ident    INT     NOT NULL,
    Classe_ident        INT     NOT NULL,
    PRIMARY KEY (Classe_ident,Ano_classe_ident),
    FOREIGN KEY (Classe_ident) REFERENCES CLASSE(Classe_ident)
);

-- ATL

CREATE TABLE ATL (
    ATL_Localizacao     INT     NOT NULL,
    Nome                INT     NOT NULL,
    PRIMARY KEY (ATL_Localizacao)
);

-- Organizado

CREATE TABLE ORGANIZADO (
    ATL_Localizacao     INT     NOT NULL,
    Classe_ident        INT     NOT NULL,
    PRIMARY KEY (ATL_Localizacao),
    FOREIGN KEY (ATL_Localizacao) REFERENCES ATL(ATL_Localizacao),
    FOREIGN KEY (Classe_ident) REFERENCES CLASSE(Classe_ident)
);

-- Professor

CREATE TABLE PROFESSOR (
    Numero_cc_pessoa     INT             NOT NULL,
    Email                VARCHAR(60),
    Contacto_tel         INT,
    Numero_func          INT             NOT NULL,
    PRIMARY KEY (Numero_cc_pessoa,Numero_func),
    FOREIGN KEY (Numero_cc_pessoa) REFERENCES PESSOA(Numero_cc)
);

-- Turma

CREATE TABLE TURMA (
    Identificador        INT             NOT NULL,
    Ano_letivo           VARCHAR(10)     NOT NULL,
    Designacao           VARCHAR(70)     NOT NULL,
    Numero_func_prof     INT             NOT NULL,
    Classe_ident         INT             NOT NULL,
    PRIMARY KEY (Identificador),
    FOREIGN KEY (Numero_func_prof) REFERENCES PROFESSOR(Numero_cc_pessoa),
    FOREIGN KEY (Classe_ident) REFERENCES CLASSE(Classe_ident)
);

-- Aluno

CREATE TABLE ALUNO (
    Numero_cc_pessoa    INT     NOT NULL,
    Turma_Id            INT     NOT NULL,
    PRIMARY KEY (Numero_cc_pessoa),
    FOREIGN KEY (Numero_cc_pessoa) REFERENCES PESSSOA(Numero_cc),
    FOREIGN KEY (Turma_Id) REFERENCES TURMA(Identificador)
);

-- Encarregado de Educação

CREATE TABLE ENCARREGADO_EDU (
    Numero_cc_adulto     INT     NOT NULL,
    Numero_cc_aluno      INT     NOT NULL,
    PRIMARY KEY (Numero_cc_adulto, Numero_cc_aluno),
    FOREIGN KEY (Numero_cc_adulto) REFERENCES ADULTO(Numero_cc_pessoa),
    FOREIGN KEY (Numero_cc_aluno) REFERENCES ALUNO(Numero_cc_pessoa)
);

