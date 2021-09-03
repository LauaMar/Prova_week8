CREATE TABLE Giocatore (
Username nvarchar(15) NOT NULL,
Psswrd nvarchar(15) NOT NULL,
IsAdmin bit NOT NULL,
CONSTRAINT PK_Giocatore PRIMARY KEY(Username)
)

CREATE TABLE Categoria (
ID int NOT NULL,
Nome nvarchar(20) NOT NULL,
CONSTRAINT PK_Categoria PRIMARY KEY(ID)
)

CREATE TABLE Arma (
ID int NOT NULL UNIQUE,
CategoriaID int NOT NULL,
Nome nvarchar(20) NOT NULL,
PuntiDanno int NOT NULL,
CONSTRAINT PK_Arma PRIMARY KEY(ID, CategoriaID),
CONSTRAINT FK_CategoriaArmaE FOREIGN KEY (CategoriaID) REFERENCES Categoria(ID),

)

CREATE TABLE Eroe (
ID int IDENTITY(1,1) NOT NULL,
Nome nvarchar(20) NOT NULL,
CategoriaID int NOT NULL,
ArmaID int NOT NULL,
Livello int NOT NULL CHECK (Livello<=5),
PuntiAccumulati int NOT NULL,
UsernameOwner nvarchar(15) NOT NULL,
CONSTRAINT PK_Ero2 PRIMARY KEY(ID),
CONSTRAINT FK_GiocatoreEroe FOREIGN KEY (UsernameOwner) REFERENCES Giocatore(Username),
CONSTRAINT FK_CategoriaEroe FOREIGN KEY (CategoriaID) REFERENCES Categoria(ID),
CONSTRAINT FK_ArmaEroe FOREIGN KEY (ArmaID, CategoriaID) REFERENCES Arma(ID, CategoriaID),
CONSTRAINT CK_Categoria CHECK (CategoriaID IN (101,102)),
CONSTRAINT Ck_Livello_Punti CHECK ((Livello=1 AND Puntiaccumulati>=0 AND PuntiAccumulati<30)OR(Livello=2 AND Puntiaccumulati>=0 AND PuntiAccumulati<60)OR(Livello=3 AND Puntiaccumulati>=0 AND PuntiAccumulati<90)OR(Livello=4 AND Puntiaccumulati>=0 AND PuntiAccumulati<120)OR(Livello=5 AND PuntiAccumulati>=0))
)

CREATE TABLE Mostro (
ID int IDENTITY(1,1) NOT NULL,
Nome nvarchar(20) NOT NULL,
CategoriaID int NOT NULL,
ArmaID int NOT NULL,
Livello int NOT NULL CHECK (Livello<=5),
CONSTRAINT PK_Mostro PRIMARY KEY(ID),
CONSTRAINT FK_CategoriaMostro FOREIGN KEY (CategoriaID) REFERENCES Categoria(ID),
CONSTRAINT FK_ArmaMostro FOREIGN KEY (ArmaID, CategoriaID) REFERENCES Arma(ID, CategoriaID),
CONSTRAINT CK_CategoriaMostro CHECK (CategoriaID IN (201,202,203))
)

