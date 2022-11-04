CREATE TABLE TipoRelacao (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(50) UNIQUE  NOT NULL   ,
PRIMARY KEY(Codigo));

CREATE TABLE TipoDeExecucao (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(40) UNIQUE  NOT NULL ,
  Binario BOOL   NOT NULL   ,
PRIMARY KEY(Codigo));

CREATE TABLE Pessoa (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(30) UNIQUE  NOT NULL ,
  Apelido VARCHAR(50)    ,
  Foto TEXT    ,
  ResearchGate VARCHAR(300)    ,
  Linkedin VARCHAR(300)    ,
  Lattes VARCHAR(300)      ,
PRIMARY KEY(Codigo));

CREATE TABLE Idioma (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(20) UNIQUE NOT NULL     ,
PRIMARY KEY(Codigo));

CREATE TABLE Localidade (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(200) UNIQUE  NOT NULL ,
  Descricao VARCHAR(500)   NOT NULL ,
  Mapa TEXT   NOT NULL ,
  X FLOAT   NOT NULL ,
  Y FLOAT   NOT NULL ,
  Z FLOAT   NOT NULL   ,
PRIMARY KEY(Codigo));

CREATE TABLE Tipo (
  Codigo SERIAL  NOT NULL ,
  TipoDeExecucao INTEGER   NOT NULL ,
  Nome VARCHAR(30) UNIQUE  NOT NULL ,
  Extensao VARCHAR(7)   NOT NULL   ,
PRIMARY KEY(Codigo)  ,
  FOREIGN KEY(TipoDeExecucao)
    REFERENCES TipoDeExecucao(Codigo));


CREATE INDEX Tipo_FKIndex1 ON Tipo (TipoDeExecucao);


CREATE INDEX IFK_Rel_27 ON Tipo (TipoDeExecucao);


CREATE TABLE Registro (
  Codigo SERIAL  NOT NULL ,
  Idioma INTEGER   NOT NULL ,
  Tipo INTEGER   NOT NULL ,
  Nome VARCHAR(30) UNIQUE  NOT NULL ,
  Apelido VARCHAR(30)    ,
  Conteudo TEXT   NOT NULL ,
  DataInsercao TIMESTAMP   NOT NULL ,
  Latitude INTEGER    ,
  Longitude INTEGER    ,
  Descricao VARCHAR(500)      ,
PRIMARY KEY(Codigo)    ,
  FOREIGN KEY(Tipo)
    REFERENCES Tipo(Codigo),
  FOREIGN KEY(Idioma)
    REFERENCES Idioma(Codigo));


CREATE INDEX Registro_FKIndex1 ON Registro (Tipo);
CREATE INDEX Registro_FKIndex2 ON Registro (Idioma);


CREATE INDEX IFK_Rel_17 ON Registro (Tipo);
CREATE INDEX IFK_Rel_23 ON Registro (Idioma);


CREATE TABLE PessoaRegistro (
  Codigo SERIAL  NOT NULL ,
  TipoRelacao INTEGER   NOT NULL ,
  Registro INTEGER   NOT NULL ,
  Pessoa INTEGER   NOT NULL   ,
PRIMARY KEY(Codigo)      ,
  FOREIGN KEY(Pessoa)
    REFERENCES Pessoa(Codigo),
  FOREIGN KEY(Registro)
    REFERENCES Registro(Codigo),
  FOREIGN KEY(TipoRelacao)
    REFERENCES TipoRelacao(Codigo));


CREATE INDEX PessoaDocumento_FKIndex1 ON PessoaRegistro (Pessoa);
CREATE INDEX PessoaDocumento_FKIndex2 ON PessoaRegistro (Registro);
CREATE INDEX PessoaDocumento_FKIndex3 ON PessoaRegistro (TipoRelacao);


CREATE INDEX IFK_Rel_09 ON PessoaRegistro (Pessoa);
CREATE INDEX IFK_Rel_10 ON PessoaRegistro (Registro);
CREATE INDEX IFK_Rel_11 ON PessoaRegistro (TipoRelacao);


CREATE TABLE RegistroLocalidade (
  Codigo SERIAL  NOT NULL ,
  Localidade INTEGER   NOT NULL ,
  Registro INTEGER   NOT NULL   ,
PRIMARY KEY(Codigo)    ,
  FOREIGN KEY(Registro)
    REFERENCES Registro(Codigo),
  FOREIGN KEY(Localidade)
    REFERENCES Localidade(Codigo));


CREATE INDEX RegistroLocalidade_FKIndex1 ON RegistroLocalidade (Registro);
CREATE INDEX RegistroLocalidade_FKIndex2 ON RegistroLocalidade (Localidade);


CREATE INDEX IFK_Rel_19 ON RegistroLocalidade (Registro);
CREATE INDEX IFK_Rel_20 ON RegistroLocalidade (Localidade);


CREATE TABLE Referencia (
  Codigo SERIAL  NOT NULL ,
  Referencia INTEGER   NOT NULL ,
  Registro INTEGER   NOT NULL   ,
PRIMARY KEY(Codigo)    ,
  FOREIGN KEY(Registro)
    REFERENCES Registro(Codigo),
  FOREIGN KEY(Referencia)
    REFERENCES Registro(Codigo));


CREATE INDEX RegistroRelacionado_FKIndex1 ON Referencia (Registro);
CREATE INDEX RegistroRelacionado_FKIndex2 ON Referencia (Referencia);


CREATE INDEX IFK_Rel_24 ON Referencia (Registro);
CREATE INDEX IFK_Rel_25 ON Referencia (Referencia);



