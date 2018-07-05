-- Script Controle Estatistico Processo DB

create database ControleEstatisticoProcesso
go

Use ControleEstatisticoProcesso
go

create table Maquina(
idMaquina int identity,
codInterno varchar(20),
modelo varchar(50) not null,
fabricante varchar(50) not null,
setor varchar(20) not null,
constraint Maquina_idMaquina_PK primary key(idMaquina),
constraint Maquina_codInterno_UQ unique(codInterno)
)
go

create table Usuario(
idUsuario int identity(100,1),
dataCadastro datetime not null,
nome varchar(50) not null,
[login] varchar(20) not null,
senha varchar(100) not null,
perfil varchar(30) not null,
setor varchar(20) not null,
ativo bit not null,
constraint Usuario_idUsuario_PK primary key(idUsuario),
constraint Usuario_login_UQ unique(login),
constraint Usuario_nome_setor_UQ unique(nome,setor)
)
go

insert into Usuario values (GETDATE(), 'Raphael Rocha', 'raphael','21232F297A57A5A743894A0E4A801FC3','Administrador','HC',1)
go

create table LimiteControle(
idLimite int identity,
dataCalculo datetime not null,
LSC decimal(5,4) not null,
LC decimal(5,4) not null,
LIC decimal(5,4) not null,
idUsuarioAprovacao int not null,
ativo bit not null,
constraint LimiteControle_idLimite_PK primary key(idLimite),
constraint LimiteControle_idUsuarioAprovacao_FK foreign key (idUsuarioAprovacao) references Usuario(idUsuario)
)
go

create table Lote(
idLote int not null,
dataHora datetime not null,
qtdTotal int not null,
qtdReprovada int not null,
percentualReprovado decimal(5,4) not null,
status varchar(30) not null,
comentario varchar(250),
idUsuarioAnalise int not null,
idUsuarioAprovacao int,
tipoLote varchar(20) not null,
idLimite int,
idMaquina int not null,
constraint Lote_idMaquina_FK foreign key (idMaquina) references Maquina(idMaquina),
constraint Lote_idLote_PK primary key(idLote),
constraint Lote_idLimite_FK foreign key (idLimite) references LimiteControle(idLimite),
constraint Lote_idUsuarioAnalise_FK foreign key (idUsuarioAnalise) references Usuario(idUsuario),
constraint Lote_idUsuarioAprovacao_FK foreign key (idUsuarioAprovacao) references Usuario(idUsuario)
)
go

create table TempLote(
idLote int not null,
constraint TempLote_idLote_PK primary key(idLote),
constraint TempLote_idLote_FK foreign key (idLote) references Lote(idLote)
)
go






-- create table Operador(
-- idOperador int identity(100,1),
-- setor varchar(20) not null,
-- nome varchar(50) not null,
-- ativo bit not null,
-- idUsuario int not null,
-- constraint Operador_idOperador_PK primary key(idOperador),
-- constraint Operador_setor_nome_UQ unique(setor,nome),
-- constraint Operador_idUsuario_FK foreign key (idUsuario) references Usuario(idUsuario)
-- )
-- go
