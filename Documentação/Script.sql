-- Script Controle Estatistico Processo DB

create database ControleEstatisticoProcesso
go

Use ControleEstatisticoProcesso
go

create table Usuario(
idUsuario int identity,
dataCadastro datetime not null,
nome varchar(20) not null,
[login] varchar(20) not null,
senha varchar(100) not null,
perfil varchar(30) not null,
ativo bit not null,
constraint Usuario_idUsuario_PK primary key(idUsuario),
constraint Usuario_login_UQ unique(login)
)
go

insert into Usuario values (GETDATE(), 'Raphael Rocha', 'raphael','21232F297A57A5A743894A0E4A801FC3','Administrador',1)

create table Operador(
idOperador int identity,
setor varchar(20) not null,
nome varchar(50) not null,
ativo bit not null,
constraint Operador_idOperador_PK primary key(idOperador),
constraint Operador_setor_nome_UQ unique(setor,nome)
)
go