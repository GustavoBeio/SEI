create database SEI_DB;

use SEI_DB;

CREATE TABLE Alunos (
    Matricula CHAR(6) PRIMARY KEY,
    Nome_Aluno VARCHAR(50) NOT NULL,
    Sobrenome_Aluno VARCHAR(50) NOT NULL,
    Login_Name_Aluno VARCHAR(20) NOT NULL UNIQUE,
	Genero_ALuno VARCHAR(30) NOT NULL,
    Data_Nascimento_Aluno DATE NOT NULL,
    Endereco VARCHAR(255) NOT NULL,
    Relacao_Responsavel VARCHAR(20) NOT NULL,
    ID_Responsavel INT NOT NULL,
    Registro_Geral_Aluno VARCHAR(10) NOT NULL UNIQUE,
    Email_Aluno VARCHAR(70) NOT NULL UNIQUE,
    Senha_Aluno VARCHAR(70) NOT NULL,
    Salt_ALuno VARCHAR(30) NOT NULL,
    FOREIGN KEY (ID_Responsavel) REFERENCES Responsaveis(ID_Responsavel)
);
-- Tabela Responsável
CREATE TABLE Responsaveis (
	ID_Responsavel INT PRIMARY KEY,
	Nome_Responsavel VARCHAR(50) NOT NULL,
    Sobrenome_Responsavel VARCHAR(50) NOT NULL,
    Telefone_Responsavel VARCHAR(20) NOT NULL,
    Login_Name_Responsavel VARCHAR(20) NOT NULL UNIQUE,
    Senha_Responsavel VARCHAR(70) NOT NULL,
    Salt_Responsavel VARCHAR(30) NOT NULL
);
-- Tabela Professores
CREATE TABLE Professores (
    ID_Professor INT AUTO_INCREMENT PRIMARY KEY,
    Nome_Professor VARCHAR(255) NOT NULL,
    Especialidade_Professor VARCHAR(255),
    Telefone_Professor VARCHAR(20),
    Email_Professor VARCHAR(70) NOT NULL UNIQUE,
    Login_Name_Professor VARCHAR(20) NOT NULL UNIQUE,
    Senha_Professor VARCHAR(70) NOT NULL,
    Salt_Professor VARCHAR(30) NOT NULL
);
-- Tabela Secretarios
CREATE TABLE Secretarios (
    ID_Secretario INT AUTO_INCREMENT PRIMARY KEY,
    Nome_Secretario VARCHAR(255) NOT NULL,
    Cargo_Secretario VARCHAR(255),
    CPF_Secretario CHAR(11) NOT NULL UNIQUE,
    Registro_Geral_Secretario VARCHAR(10) NOT NULL UNIQUE,
    Telefone_Secretario VARCHAR(20),
    Login_Name_Secretario VARCHAR(20)NOT NULL UNIQUE,
    Senha_Secretario VARCHAR(70) NOT NULL,
    Salt_Secretario VARCHAR(30) NOT NULL
);
-- Tabela Disciplinas
CREATE TABLE Disciplinas (
    ID_Disciplina INT AUTO_INCREMENT PRIMARY KEY,
    Nome_Disciplina VARCHAR(255) NOT NULL,
    Carga_Horaria INT,
    Professor_Responsavel INT,
    FOREIGN KEY (Professor_Responsavel) REFERENCES Professores(ID_Professor)
);
-- Tabela Turmas
CREATE TABLE Turmas (
    ID_Turma INT AUTO_INCREMENT PRIMARY KEY,
    Nome_Turma VARCHAR(255) NOT NULL,
    Ano_Letivo YEAR,
    Professor_Orientador INT,
    FOREIGN KEY (Professor_Orientador) REFERENCES Professores(ID_Professor)
);
-- Tabela Matriculas
CREATE TABLE Matriculas (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Matricula_Aluno CHAR(6),
    ID_Turma INT,
    FOREIGN KEY (Matricula_Aluno) REFERENCES Alunos(Matricula),
    FOREIGN KEY (ID_Turma) REFERENCES Turmas(ID_Turma)
);
-- Tabela Notas
CREATE TABLE Notas (
    ID_Notas INT AUTO_INCREMENT PRIMARY KEY,
    Matricula_Aluno CHAR(6),
    ID_Disciplina INT,
    Nota DECIMAL(3, 1),
    FOREIGN KEY (Matricula_Aluno) REFERENCES Alunos(Matricula),
    FOREIGN KEY (ID_Disciplina) REFERENCES Disciplinas(ID_Disciplina)
);
-- Tabela Frequencia
CREATE TABLE Frequencia (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Matricula_Aluno CHAR(6),
    ID_Disciplina INT,
    Presencas INT,
    Faltas INT,
    FOREIGN KEY (Matricula_Aluno) REFERENCES Alunos(Matricula),
    FOREIGN KEY (ID_Disciplina) REFERENCES Disciplinas(ID_Disciplina)
);
-- Tabela Eventos
CREATE TABLE Eventos (
    ID_Evento INT AUTO_INCREMENT PRIMARY KEY,
    Nome_Evento VARCHAR(255) NOT NULL,
    Data_Evento DATE,
    Descricao TEXT,
    Autor_Evento VARCHAR(50)
);
DELIMITER //
CREATE PROCEDURE CadastrarAluno(
    IN p_Nome_Aluno VARCHAR(50),
    IN p_Sobrenome_Aluno VARCHAR(50),
    IN p_Login_Name_Aluno VARCHAR(20),
    IN p_Genero_Aluno VARCHAR(30),
    IN p_Data_Nascimento_Aluno DATE,
    IN p_Endereco VARCHAR(255),
    IN p_Relacao_Responsavel VARCHAR(20),
    IN p_ID_Responsavel INT,
    IN p_Registro_Geral_Aluno VARCHAR(10),
    IN p_Email_Aluno VARCHAR(70),
    IN p_Senha_Aluno VARCHAR(70),
    IN p_Salt_Aluno VARCHAR(30)
)
BEGIN
	DECLARE novaMatricula CHAR(6);
    SET novaMatricula = GerarMatricula(); -- Chama a função GerarMatricula
    -- Tente inserir o novo aluno
    INSERT INTO Alunos (
        Matricula, 
        Nome_Aluno, 
        Sobrenome_Aluno, 
        Login_Name_ALuno,
        Genero_Aluno, 
        Data_Nascimento_Aluno, 
        Endereco, 
        Relacao_Responsavel, 
        ID_Responsavel, 
        Registro_Geral_Aluno, 
        Email_Aluno, 
        Senha_Aluno, 
        Salt_Aluno
    ) VALUES (
        novaMatricula, 
        p_Nome_Aluno, 
        p_Sobrenome_Aluno,
        P_Login_Name_Aluno,
        p_Genero_Aluno, 
        p_Data_Nascimento_Aluno, 
        p_Endereco, 
        p_Relacao_Responsavel, 
        p_ID_Responsavel, 
        p_Registro_Geral_Aluno, 
        p_Email_Aluno, 
        p_Senha_Aluno, 
        p_Salt_Aluno
    );
END //
DELIMITER ;

DELIMITER //
CREATE FUNCTION GerarMatricula() RETURNS CHAR(6)
BEGIN
    DECLARE novaMatricula CHAR(6);
    SET novaMatricula = LPAD(CONVERT(RAND() * 1000000, UNSIGNED), 6, '0');

    WHILE EXISTS (SELECT 1 FROM Alunos WHERE Matricula = novaMatricula) DO
        SET novaMatricula = LPAD(CONVERT(RAND() * 1000000, UNSIGNED), 6, '0');
    END WHILE;

    RETURN novaMatricula;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE BuscarAluno(
	IN p_Matricula CHAR(6),
    OUT p_Nome_Aluno VARCHAR(50),
    OUT p_Sobrenome_Aluno VARCHAR(50),
    OUT p_Login_Name_Aluno VARCHAR(20),
    OUT p_Genero_Aluno VARCHAR(30),
    OUT p_Data_Nascimento_Aluno DATE,
    OUT p_Endereco VARCHAR(255),
    OUT p_Relacao_Responsavel VARCHAR(20),
    OUT p_ID_Responsavel INT,
    OUT p_Registro_Geral_Aluno VARCHAR(10),
    OUT p_Email_Aluno VARCHAR(70),
    OUT p_Senha_Aluno VARCHAR(70),
    OUT p_Salt_Aluno VARCHAR(30)
)
BEGIN
	SELECT * FROM Alunos WHERE Matricula = p_Matricula LIMIT 1;
END //

DELIMITER //
CREATE PROCEDURE AtualizarAluno(
    IN p_Matricula CHAR(6),
    IN p_Nome_Aluno VARCHAR(50),
    IN p_Sobrenome_Aluno VARCHAR(50),
    IN p_Login_Name_Aluno VARCHAR(20),
    IN p_Genero_ALuno VARCHAR(30),
    IN p_Data_Nascimento_Aluno DATE,
    IN p_Endereco VARCHAR(255),
    IN p_Relacao_Responsavel VARCHAR(20),
    IN p_ID_Responsavel INT,
    IN p_Registro_Geral_Aluno VARCHAR(10),
    IN p_Email_Aluno VARCHAR(70)
)
BEGIN
    UPDATE Alunos
    SET
        Nome_Aluno = p_Nome_Aluno,
        Sobrenome_Aluno = p_Sobrenome_Aluno,
        Login_Name_Aluno = p_Login_Name_Aluno,
        Genero_ALuno = p_Genero_ALuno,
        Data_Nascimento_Aluno = p_Data_Nascimento_Aluno,
        Endereco = p_Endereco,
        Relacao_Responsavel = p_Relacao_Responsavel,
        ID_Responsavel = p_ID_Responsavel,
        Registro_Geral_Aluno = p_Registro_Geral_Aluno,
        Email_Aluno = p_Email_Aluno
    WHERE Matricula = p_Matricula;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE RecuperarCredAluno(
    IN p_Login_Name_Aluno VARCHAR(20),
    OUT p_Salt_Aluno VARCHAR(30)
)
BEGIN
    SELECT Salt_Aluno
    INTO p_Salt_Aluno
    FROM Alunos
    WHERE Login_Name_Aluno = p_Login_Name_Aluno;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE ExcluirAluno(
    IN p_Matricula CHAR(6)
)
BEGIN
    DELETE FROM Alunos
    WHERE Matricula = p_Matricula;
END //
DELIMITER ;

