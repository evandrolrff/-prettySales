CREATE TABLE Users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(100) NOT NULL,
    lastName VARCHAR(100) NOT NULL,
    address VARCHAR(255) NOT NULL,
    number VARCHAR(10) NOT NULL,
    complement VARCHAR(255) NULL
);

INSERT INTO Users (name, lastName, address, number, complement) VALUES
('João', 'Silva', 'Rua ABC', '123', 'Apto 2B'),
('Maria', 'Souza', 'Avenida XPTO', '456', NULL),
('Carlos', 'Pereira', 'Rua das Flores', '789', 'Casa 10'),
('Ana', 'Oliveira', 'Rua das Palmeiras', '321', 'Bloco B'),
('Bruno', 'Ferreira', 'Avenida Central', '654', 'Sala 2'),
('Fernanda', 'Almeida', 'Rua São João', '101', 'Casa 1'),
('Rodrigo', 'Melo', 'Avenida das Nações', '202', NULL),
('Tatiane', 'Ribeiro', 'Travessa dos Pinheiros', '303', 'Bloco C, Apto 12'),
('Leonardo', 'Costa', 'Rua Bela Vista', '404', 'Sala 5'),
('Amanda', 'Martins', 'Avenida Independência', '505', 'Casa Fundos');

CREATE TABLE Products (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(150) NOT NULL,
    description TEXT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    pathImage VARCHAR(255) NULL
);

INSERT INTO Products (name, description, price, pathImage) VALUES
('Notebook Dell', 'Notebook Core i7, 16GB RAM, SSD 512GB', 4500.00, 'images/notebook_dell.jpg'),
('Smartphone Samsung', 'Smartphone 128GB, câmera 48MP', 2500.00, 'images/smartphone_samsung.jpg'),
('Mouse Gamer', 'Mouse com 7 botões programáveis', 150.00, 'images/mouse_gamer.jpg'),
('Teclado Mecânico', 'Teclado RGB com switches mecânicos', 350.00, 'images/teclado_mecanico.jpg'),
('Monitor LG 27"', 'Monitor Full HD, 144Hz, IPS', 1200.00, 'images/monitor_lg.jpg'),
('Tablet Lenovo', 'Tablet 10" com processador Octa-Core e 64GB', 1800.00, 'images/tablet_lenovo.jpg'),
('Fone de Ouvido JBL', 'Fone de ouvido Bluetooth com cancelamento de ruído', 400.00, 'images/fone_jbl.jpg'),
('Cadeira Gamer', 'Cadeira ergonômica com ajuste de altura e inclinação', 1500.00, 'images/cadeira_gamer.jpg'),
('HD Externo 1TB', 'HD externo USB 3.0 com 1TB de armazenamento', 350.00, 'images/hd_externo.jpg'),
('Impressora HP', 'Impressora multifuncional com conexão Wi-Fi', 850.00, 'images/impressora_hp.jpg');


CREATE TABLE Sales (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userId INTEGER NOT NULL,
    productId INTEGER NOT NULL,
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    saleDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (userId) REFERENCES Users(id) ON DELETE CASCADE,
    FOREIGN KEY (productId) REFERENCES Products(id) ON DELETE CASCADE
);

INSERT INTO Sales (userId, productId, quantity, saleDate) VALUES
(2, 2, 1, '2024-02-15 10:30:00'),  -- João comprou 1 Smartphone Samsung
(3, 4, 2, '2024-02-16 14:45:00'),  -- Maria comprou 2 Teclados Mecânicos
(4, 5, 1, '2024-02-17 09:20:00'), -- Carlos comprou 1 Monitor LG 27"
(5, 1, 1, '2024-02-18 16:10:00'), -- Ana comprou 1 Notebook Dell
(6, 3, 3, '2024-02-19 11:05:00'),  -- Bruno comprou 3 Mouses Gamer
(7, 6, 1, '2024-02-20 13:30:00'), -- Fernanda comprou 1 Tablet Lenovo
(8, 7, 1, '2024-02-21 15:40:00'),  -- Rodrigo comprou 1 Fone JBL
(9, 8, 2, '2024-02-22 17:50:00'); -- Tatiane comprou 2 Cadeiras Gamer

CREATE TABLE Payments (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userId INTEGER NOT NULL,
    saleId INTEGER NULL, -- Pode ser NULL para pagamentos não vinculados a uma venda específica
    amount DECIMAL(10,2) NOT NULL CHECK (amount > 0),
    paymentDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (userId) REFERENCES Users(id) ON DELETE CASCADE,
    FOREIGN KEY (saleId) REFERENCES Sales(id) ON DELETE CASCADE
);

INSERT INTO Payments (userId, saleId, amount, paymentDate) VALUES
(2, 1, 1000.00, '2024-02-15 12:00:00'), -- João pagou R$ 1000,00 da compra do Smartphone
(3, 2, 700.00, '2024-02-16 15:30:00'),  -- Maria quitou totalmente a compra dos Teclados Mecânicos
(4, 3, 500.00, '2024-02-17 10:00:00'),  -- Carlos pagou R$ 500,00 do Monitor LG
(5, 4, 2500.00, '2024-02-18 18:00:00'), -- Ana pagou R$ 2500,00 do Notebook Dell
(6, 5, 450.00, '2024-02-19 12:30:00'),  -- Bruno quitou totalmente a compra dos Mouses Gamer
(7, 6, 900.00, '2024-02-20 14:00:00'),  -- Fernanda pagou metade do Tablet Lenovo
(8, NULL, 500.00, '2024-02-21 16:00:00'), -- Rodrigo fez um pagamento sem estar vinculado a uma venda específica
(9, 8, 1000.00, '2024-02-22 18:30:00'); -- Tatiane pagou R$ 1000,00 das Cadeiras Gamer
