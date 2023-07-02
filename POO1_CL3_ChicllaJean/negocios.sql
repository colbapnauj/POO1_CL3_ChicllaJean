CREATE DATABASE Negocios

USE negocios

CREATE TABLE cliente
	(
		idCliente INT primary key AUTO_INCREMENT,
		nombres VARCHAR(120),
		tipoDocumento INT,
		documento VARCHAR(30),
		telefono varchar(30)
		);
		
USE negocios

CREATE PROCEDURE usp_clientes
AS
Select idCliente, nombres, tipoDocumento, documento, telefono 
from cliente

DELIMITER //
CREATE PROCEDURE usp_clientes()
BEGIN
    SELECT idCliente, nombres, tipoDocumento, documento, telefono 
    FROM cliente;
END //
DELIMITER ;

call usp_clientes()

DELIMITER //
CREATE PROCEDURE usp_registrar_cliente(
    IN p_nombres VARCHAR(100),
    IN p_tipoDocumento VARCHAR(50),
    IN p_documento VARCHAR(50),
    IN p_telefono VARCHAR(20)
)
BEGIN
    INSERT INTO cliente (nombres, tipoDocumento, documento, telefono)
    VALUES (p_nombres, p_tipoDocumento, p_documento, negocios);
END //
DELIMITER ;

-- edit
DELIMITER //
CREATE PROCEDURE usp_editar_cliente(
    IN p_idCliente INT,
    IN p_nombres VARCHAR(100),
    IN p_tipoDocumento VARCHAR(50),
    IN p_documento VARCHAR(50),
    IN p_telefono VARCHAR(20)
)
BEGIN
    UPDATE cliente
    SET nombres = p_nombres,
        tipoDocumento = p_tipoDocumento,
        documento = p_documento,
        telefono = p_telefono
    WHERE idCliente = p_idCliente;
END //
DELIMITER ;

-- delete
DELIMITER //
CREATE PROCEDURE usp_eliminar_cliente(
    IN p_idCliente INT
)
BEGIN
    DELETE FROM cliente
    WHERE idCliente = p_idCliente;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE usp_buscar_cliente_por_nombre(
    IN p_nombre VARCHAR(100)
)
BEGIN
    SELECT idCliente, nombres, tipoDocumento, documento, telefono 
    FROM cliente
    WHERE nombres LIKE CONCAT('%', p_nombre, '%');
END //
DELIMITER ;

CALL usp_buscar_cliente_por_nombre('j')