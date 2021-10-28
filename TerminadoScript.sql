create database Farmacia;
use Farmacia;
-- ---------------------------
-- Criando tabelas Farmácia
-- ----------------------------
CREATE TABLE IF NOT EXISTS `Comprador` (
  `RG` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(60) NOT NULL,
  `Cidade` VARCHAR(45) NOT NULL,
  `Endereço` VARCHAR(60) NOT NULL,
  `UF` VARCHAR(2) NOT NULL,
  `OrgaoEmissor` VARCHAR(3) NOT NULL,
  PRIMARY KEY (`RG`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Emitente` (
  `codigo` INT NOT NULL,
  `nome` VARCHAR(60) NOT NULL,
  `endereco` VARCHAR(60) NOT NULL,
  `cidade` VARCHAR(45) NOT NULL,
  `telefone` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`codigo`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `ReceitaMedica` (
  `data` INT NOT NULL AUTO_INCREMENT,
  `tipo` VARCHAR(45) NOT NULL,
  `crm` VARCHAR(45) NOT NULL,
  `prescricao` VARCHAR(45) NOT NULL,
  `Emitente_codigo` INT NOT NULL,
  `Comprador_RG` INT NOT NULL,
  PRIMARY KEY (`data`),
  INDEX `fk_ReceitaMedica_Emitente1_idx` (`Emitente_codigo` ASC) VISIBLE,
  INDEX `fk_ReceitaMedica_Comprador1_idx` (`Comprador_RG` ASC) VISIBLE,
  CONSTRAINT `fk_ReceitaMedica_Emitente1`
    FOREIGN KEY (`Emitente_codigo`)
    REFERENCES `Emitente` (`codigo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ReceitaMedica_Comprador1`
    FOREIGN KEY (`Comprador_RG`)
    REFERENCES `Comprador` (`RG`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Fornecedor` (
  `Matricula` INT NOT NULL AUTO_INCREMENT,
  `localizacao` VARCHAR(100) NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  `lote` INT NOT NULL,
  PRIMARY KEY (`Matricula`))
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `Produto` (
  `codigo_de_Barras` INT NOT NULL AUTO_INCREMENT,
  `estoque` INT NOT NULL,
  `valor` FLOAT NOT NULL,
  `Fornecedor_Matricula` INT NOT NULL,
  PRIMARY KEY (`codigo_de_Barras`),
  INDEX `fk_Produto_Fornecedor1_idx` (`Fornecedor_Matricula` ASC) VISIBLE,
  CONSTRAINT `fk_Produto_Fornecedor1`
    FOREIGN KEY (`Fornecedor_Matricula`)
    REFERENCES `Fornecedor` (`Matricula`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `Medicamento` (
  `codigo` INT NOT NULL AUTO_INCREMENT,
  `laboratorio` VARCHAR(45) NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  `composicao` VARCHAR(45) NOT NULL,
  `tarja` VARCHAR(20) NOT NULL,
  `tipo` VARCHAR(45) NOT NULL,
  `Produto_codigo_de_Barras` INT NOT NULL,
  PRIMARY KEY (`codigo`),
  INDEX `fk_Medicamento_Produto1_idx` (`Produto_codigo_de_Barras` ASC) VISIBLE,
  UNIQUE INDEX `Produto_codigo_de_Barras_UNIQUE` (`Produto_codigo_de_Barras` ASC) VISIBLE,
  CONSTRAINT `fk_Medicamento_Produto1`
    FOREIGN KEY (`Produto_codigo_de_Barras`)
    REFERENCES `Produto` (`codigo_de_Barras`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Cosmetico` (
  `codigo` INT NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(60) NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  `marca` VARCHAR(45) NOT NULL,
  `tipo` VARCHAR(45) NOT NULL,
  `Produto_codigo_de_Barras` INT NOT NULL,
  PRIMARY KEY (`codigo`),
  INDEX `fk_Cosmetico_Produto1_idx` (`Produto_codigo_de_Barras` ASC) VISIBLE,
  UNIQUE INDEX `Produto_codigo_de_Barras_UNIQUE` (`Produto_codigo_de_Barras` ASC) VISIBLE,
  CONSTRAINT `fk_Cosmetico_Produto1`
    FOREIGN KEY (`Produto_codigo_de_Barras`)
    REFERENCES `Produto` (`codigo_de_Barras`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `CuidadoPessoal` (
  `codigo` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `marca` VARCHAR(45) NOT NULL,
  `tipo` VARCHAR(45) NOT NULL,
  `descricao` VARCHAR(60) NOT NULL,
  `Produto_codigo_de_Barras` INT NOT NULL,
  PRIMARY KEY (`codigo`),
  INDEX `fk_CuidadoPessoal_Produto1_idx` (`Produto_codigo_de_Barras` ASC) VISIBLE,
  UNIQUE INDEX `Produto_codigo_de_Barras_UNIQUE` (`Produto_codigo_de_Barras` ASC) VISIBLE,
  CONSTRAINT `fk_CuidadoPessoal_Produto1`
    FOREIGN KEY (`Produto_codigo_de_Barras`)
    REFERENCES `Produto` (`codigo_de_Barras`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `Loja` (
  `numero` INT NOT NULL AUTO_INCREMENT,
  `cidade` VARCHAR(45) NOT NULL,
  `endereco` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`numero`))
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `Fornecedor_has_Loja` (
  `Fornecedor_Matricula` INT NOT NULL,
  `Loja_numero` INT NOT NULL,
  PRIMARY KEY (`Fornecedor_Matricula`, `Loja_numero`),
  INDEX `fk_Fornecedor_has_Loja_Loja1_idx` (`Loja_numero` ASC) VISIBLE,
  INDEX `fk_Fornecedor_has_Loja_Fornecedor1_idx` (`Fornecedor_Matricula` ASC) VISIBLE,
  CONSTRAINT `fk_Fornecedor_has_Loja_Fornecedor1`
    FOREIGN KEY (`Fornecedor_Matricula`)
    REFERENCES `Fornecedor` (`Matricula`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Fornecedor_has_Loja_Loja1`
    FOREIGN KEY (`Loja_numero`)
    REFERENCES `Loja` (`numero`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `Funcionario` (
  `matricula` INT NOT NULL AUTO_INCREMENT,
  `nascimento` VARCHAR(45) NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  `cargo` VARCHAR(45) NOT NULL,
  `sexo` VARCHAR(45) NOT NULL,
  `salario` VARCHAR(45) NOT NULL,
  `Loja_numero` INT NOT NULL,
  PRIMARY KEY (`matricula`),
  INDEX `fk_Funcionario_Loja1_idx` (`Loja_numero` ASC) VISIBLE,
  CONSTRAINT `fk_Funcionario_Loja1`
    FOREIGN KEY (`Loja_numero`)
    REFERENCES `Loja` (`numero`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `ClienteCadastrado` (
  `CPF` INT NOT NULL AUTO_INCREMENT,
  `NotaFiscal` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`CPF`))
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `Produto_has_ClienteCadastrado` (
  `Produto_codigo_de_Barras` INT NOT NULL,
  `ClienteCadastrado_CPF` INT NOT NULL,
  PRIMARY KEY (`Produto_codigo_de_Barras`, `ClienteCadastrado_CPF`),
  INDEX `fk_Produto_has_ClienteCadastrado_ClienteCadastrado1_idx` (`ClienteCadastrado_CPF` ASC) VISIBLE,
  INDEX `fk_Produto_has_ClienteCadastrado_Produto1_idx` (`Produto_codigo_de_Barras` ASC) VISIBLE,
  CONSTRAINT `fk_Produto_has_ClienteCadastrado_Produto1`
    FOREIGN KEY (`Produto_codigo_de_Barras`)
    REFERENCES `Produto` (`codigo_de_Barras`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Produto_has_ClienteCadastrado_ClienteCadastrado1`
    FOREIGN KEY (`ClienteCadastrado_CPF`)
    REFERENCES `ClienteCadastrado` (`CPF`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


CREATE TABLE IF NOT EXISTS `Medicamento_has_ReceitaMedica` (
  `Medicamento_codigo` INT NOT NULL,
  `ReceitaMedica_data` INT NOT NULL,
  PRIMARY KEY (`Medicamento_codigo`, `ReceitaMedica_data`),
  INDEX `fk_Medicamento_has_ReceitaMedica_ReceitaMedica1_idx` (`ReceitaMedica_data` ASC) VISIBLE,
  INDEX `fk_Medicamento_has_ReceitaMedica_Medicamento1_idx` (`Medicamento_codigo` ASC) VISIBLE,
  CONSTRAINT `fk_Medicamento_has_ReceitaMedica_Medicamento1`
    FOREIGN KEY (`Medicamento_codigo`)
    REFERENCES `Medicamento` (`codigo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Medicamento_has_ReceitaMedica_ReceitaMedica1`
    FOREIGN KEY (`ReceitaMedica_data`)
    REFERENCES `ReceitaMedica` (`data`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -------------------------
-- Criando View ------------
-- -------------------------
create view vw_medicamento_preco as select  produto.valor, medicamento.nome
from produto inner join medicamento on produto.codigo_de_Barras = Produto_codigo_de_Barras;

-- -------------------------------
-- Criando Procedure 
-- --------------------------------

DELIMITER $$
create procedure soma_estoque(estoque int)
begin
	select estoque from produto where estoque<=10;
    if estoque <=10 then 
    select *from fornecedor;
    end if;
end $$
DELIMITER ;  

-- -------------------------------
-- Criando Dado Binário - Imagem 
-- --------------------------------
CREATE TABLE DadoBinario (
codigo INT NOT NULL PRIMARY KEY,
Imagem MEDIUMBLOB);
show tables;
INSERT INTO DadoBinario (codigo ,Imagem) VALUES (2,LOAD_FILE("C:\Users\laisportela\Desktop\imagem_para_banco"));


-- -------------------------------
-- Inserindo Dados 
-- --------------------------------

use farmacia;
select *from loja;
select *from funcionario;
select *from fornecedor;
select *from produto;
select *from medicamento;
insert into loja (cidade, endereco) values ("Samambaia Norte", "Qr 412 conj 7, lote 2");
insert into loja (cidade, endereco) values ("Samambaia Sul", "Qr 316 conj 9, lote 40");
insert into loja (cidade, endereco) values ("Taguatinga", "Qna30");
insert into loja (cidade, endereco) values ("Taguatinga Centro", "Alameda Shopping Piso 1");
insert into loja (cidade, endereco) values ("Samambaia Norte", "Qr 403 conj 2 lote 30");

insert into Funcionario (nascimento, nome, cargo, sexo, salario, loja_numero) values ("31/07/2000", "Larissa Gouveia", "atendente", "feminino", "3000", 1);
insert into Funcionario (nascimento, nome, cargo, sexo, salario, loja_numero) values ("03/07/1980", "Paulo Machado", "farmaceutico", "masculino", "4000", 2);
insert into Funcionario (nascimento, nome, cargo, sexo, salario, loja_numero) values ("01/01/1990", "Juliana Pereira", "gerente", "feminino", "5000", 3);
insert into Funcionario (nascimento, nome, cargo, sexo, salario, loja_numero) values ("11/11/1995", "Pedro Lucas", "atendente", "masculino", "3000", 4);
insert into Funcionario (nascimento, nome, cargo, sexo, salario, loja_numero) values ("03/02/2002", "Luis Matheus", "Limpeza", "masculino", "2000", 5);
select *from medicamento;

insert into Fornecedor (localizacao, nome, lote) values ("Setor de Industrial Sul ", "Farmaco Vende", 400);
insert into Fornecedor (localizacao, nome, lote) values ("Taguatinga Sul  ", "Farmaco Tagua", 2000);
insert into Fornecedor (localizacao, nome, lote) values ("Ceilândia Norte ", "LUIS Soluções", 880);
insert into Fornecedor (localizacao, nome, lote) values ("Guara II ", "Guara Cosméticos", 600);
insert into Fornecedor (localizacao, nome, lote) values ("Recanto das Emas ", "Farma Utilidades", 400);

insert into Produto ( estoque, valor, Fornecedor_Matricula) values (55 , 10.5, 4);
insert into Produto ( estoque, valor, Fornecedor_Matricula) values (500 , 9.99, 2);
insert into Produto ( estoque, valor, Fornecedor_Matricula) values (300 , 40, 1);
insert into Produto ( estoque, valor, Fornecedor_Matricula) values (10 , 88, 3);
insert into Produto ( estoque, valor, Fornecedor_Matricula) values (34 , 22.5, 5);

insert into Medicamento ( laboratorio, nome, composicao, tarja, tipo, produto_codigo_de_barras) values ("Eurofarma", "Carbolitium", "Carbonato de Lítio", "Vermelha", "Ansiolítico",1 );
insert into Medicamento ( laboratorio, nome, composicao, tarja, tipo, produto_codigo_de_barras) values ("Eurofarma", "Venlafaxin", "Cloridrato de Venlafaxina", "Vermelha", "Ansiolítico",2 );
insert into Medicamento ( laboratorio, nome, composicao, tarja, tipo, produto_codigo_de_barras) values ("Eurofarma", "neosaldina", "Paracetamol e Cafeina ", "Sem Tarja", "Analgésico",3 );
insert into Medicamento ( laboratorio, nome, composicao, tarja, tipo, produto_codigo_de_barras) values ("Eurofarma", "Rivotril", "Rivotril", "Preta", "Ansiolítico",4 );
insert into Medicamento ( laboratorio, nome, composicao, tarja, tipo, produto_codigo_de_barras) values ("Eurofarma", "Dorflex", "Diporona", "Sem Tarja", "Analgésico",5 );

