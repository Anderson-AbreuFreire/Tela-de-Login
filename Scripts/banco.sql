CREATE TABLE IF NOT EXISTS departamento (
    id_departamento SERIAL PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO departamento (id_departamento, nome) VALUES
    (1, 'RH'),
    (2, 'Produção'),
    (3, 'Gerência')
ON CONFLICT (id_departamento) DO NOTHING;

CREATE TABLE IF NOT EXISTS funcionario (
    id_funcionario SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    senha VARCHAR(100) NOT NULL,
    id_departamento INTEGER NOT NULL REFERENCES departamento(id_departamento) ON DELETE RESTRICT
);

CREATE TABLE IF NOT EXISTS chamado (
    id_chamado SERIAL PRIMARY KEY,
    data_abertura TIMESTAMP NOT NULL,
    status VARCHAR(20) NOT NULL,
    id_funcionario INTEGER NOT NULL REFERENCES funcionario(id_funcionario) ON DELETE CASCADE,
    categoria VARCHAR(100) NOT NULL,
    titulo VARCHAR(200) NOT NULL,
    descricao TEXT
);
