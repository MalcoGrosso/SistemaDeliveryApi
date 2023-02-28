-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 28-02-2023 a las 22:29:32
-- Versión del servidor: 10.4.24-MariaDB
-- Versión de PHP: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sistemadelivery6`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallepedido`
--

CREATE TABLE `detallepedido` (
  `idDetallePedido` int(20) NOT NULL,
  `idProductoDP` int(20) NOT NULL,
  `precioPedido` double NOT NULL,
  `IdentificadorDetallePedido` int(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `detallepedido`
--

INSERT INTO `detallepedido` (`idDetallePedido`, `idProductoDP`, `precioPedido`, `IdentificadorDetallePedido`) VALUES
(222, 1, 500, 63),
(223, 1, 500, 63),
(224, 2, 1200, 63),
(225, 2, 1200, 63),
(231, 1, 500, 64),
(232, 1, 500, 64),
(235, 1, 500, 65),
(236, 1, 500, 65),
(237, 2, 1200, 65),
(238, 3, 300, 65),
(239, 4, 700, 66),
(240, 4, 700, 66),
(241, 5, 300, 66),
(242, 5, 300, 66),
(243, 1, 500, 68),
(244, 1, 500, 68),
(245, 1, 500, 69),
(246, 1, 500, 69),
(247, 4, 700, 69),
(248, 4, 700, 69),
(249, 1, 500, 70),
(250, 1, 500, 70),
(251, 5, 300, 70),
(252, 5, 300, 70),
(253, 1, 500, 71),
(254, 1, 500, 71),
(255, 3, 300, 71),
(256, 3, 300, 71),
(257, 1, 500, 77),
(258, 1, 500, 77),
(259, 1, 500, 77),
(260, 2, 1200, 79),
(261, 2, 1200, 79),
(262, 4, 700, 80),
(263, 5, 300, 80),
(267, 1, 500, 82),
(268, 1, 500, 82),
(269, 1, 500, 82),
(270, 1, 500, 82),
(271, 1, 500, 82),
(272, 1, 500, 82),
(273, 1, 500, 78),
(274, 1, 500, 78),
(275, 2, 1200, 78),
(276, 2, 1200, 78),
(277, 3, 300, 78),
(278, 2, 1200, 84),
(279, 2, 1200, 84),
(280, 4, 700, 84),
(281, 4, 700, 84),
(284, 2, 1200, 85),
(285, 2, 1200, 85),
(286, 3, 300, 85),
(287, 3, 300, 85),
(288, 5, 300, 85),
(289, 5, 300, 85),
(292, 1, 500, 86),
(293, 1, 500, 86),
(294, 1, 500, 86),
(296, 3, 300, 86),
(298, 4, 700, 86),
(299, 4, 700, 86),
(301, 5, 300, 86),
(302, 2, 1200, 86),
(322, 1, 500, 88),
(323, 1, 500, 88),
(324, 4, 700, 88),
(325, 4, 700, 88),
(326, 2, 1200, 88),
(327, 4, 700, 89),
(328, 4, 700, 89),
(329, 1, 500, 89),
(330, 1, 500, 89),
(331, 1, 500, 90),
(332, 1, 500, 90),
(333, 2, 1200, 90),
(334, 2, 1200, 90),
(335, 1, 500, 91),
(336, 1, 500, 91),
(337, 5, 300, 91),
(338, 5, 300, 91),
(339, 4, 700, 91),
(340, 4, 700, 91);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `empleados`
--

CREATE TABLE `empleados` (
  `idEmpleados` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `direccion` varchar(70) NOT NULL,
  `email` varchar(70) NOT NULL,
  `password` varchar(100) NOT NULL,
  `telefono` varchar(30) NOT NULL,
  `tipo` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `empleados`
--

INSERT INTO `empleados` (`idEmpleados`, `nombre`, `apellido`, `direccion`, `email`, `password`, `telefono`, `tipo`) VALUES
(1, 'Mariano', 'Luzza', 'asd 123', 'ml@gmail.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '2664735945', ''),
(2, 'Nadie', 'Nadie', 'Ninguna', 'No', '123', '00000', '0');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `idPago` int(30) NOT NULL,
  `idPedidoPago` int(30) NOT NULL,
  `fechaPago` datetime NOT NULL DEFAULT current_timestamp(),
  `idTipoPagoP` int(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`idPago`, `idPedidoPago`, `fechaPago`, `idTipoPagoP`) VALUES
(39, 63, '2023-02-09 14:08:47', 3),
(40, 64, '2023-02-09 14:24:31', 1),
(41, 65, '2023-02-09 14:25:11', 1),
(42, 66, '2023-02-09 14:25:55', 2),
(43, 68, '2023-02-09 14:35:26', 1),
(44, 69, '2023-02-09 15:12:49', 3),
(45, 70, '2023-02-09 15:25:29', 3),
(46, 71, '2023-02-09 15:31:36', 5),
(47, 77, '2023-02-09 20:15:23', 1),
(48, 79, '2023-02-09 20:22:41', 1),
(49, 82, '2023-02-11 09:32:20', 2),
(50, 78, '2023-02-14 15:24:06', 3),
(51, 84, '2023-02-14 15:38:52', 3),
(52, 85, '2023-02-16 16:35:18', 1),
(53, 86, '2023-02-16 17:29:59', 1),
(54, 88, '2023-02-18 11:15:45', 1),
(55, 90, '2023-02-28 18:19:34', 3),
(56, 91, '2023-02-28 18:20:18', 4);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pedidos`
--

CREATE TABLE `pedidos` (
  `idPedido` int(20) NOT NULL,
  `idEmpleadoPedido` int(20) NOT NULL,
  `fechaPedido` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `fechaEntrega` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `estado` int(5) NOT NULL,
  `idUsuarioPedido` int(20) NOT NULL,
  `latitudPedido` varchar(30) NOT NULL,
  `longitudPedido` varchar(30) NOT NULL,
  `montoFinal` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `pedidos`
--

INSERT INTO `pedidos` (`idPedido`, `idEmpleadoPedido`, `fechaPedido`, `fechaEntrega`, `estado`, `idUsuarioPedido`, `latitudPedido`, `longitudPedido`, `montoFinal`) VALUES
(63, 2, '2023-02-09 17:07:28', '2023-02-09 17:07:13', 1, 1, '-1', '-1', 3400),
(64, 2, '2023-02-09 17:24:00', '2023-02-09 17:09:29', 1, 1, '-1', '-1', 1000),
(65, 2, '2023-02-09 17:24:57', '2023-02-09 17:24:52', 1, 1001, '-1', '-1', 2500),
(66, 2, '2023-02-09 17:25:21', '2023-02-09 17:25:21', 1, 1001, '-1', '-1', 2000),
(67, 2, '2023-02-09 17:26:07', '2023-02-09 17:26:07', 0, 1001, '-1', '-1', 0),
(68, 2, '2023-02-09 17:35:06', '2023-02-09 17:35:06', 1, 1, '-1', '-1', 1000),
(69, 2, '2023-02-09 18:12:39', '2023-02-09 17:35:29', 1, 1, '-1', '-1', 2400),
(70, 2, '2023-02-09 18:25:15', '2023-02-09 18:25:01', 1, 1002, '-1', '-1', 1600),
(71, 2, '2023-02-09 18:31:21', '2023-02-09 18:27:34', 1, 1002, '-33.2152213', '-66.2355196', 1600),
(72, 2, '2023-02-09 20:58:11', '2023-02-09 20:58:11', 0, 1002, '-1', '-1', 0),
(73, 2, '2023-02-09 20:59:42', '2023-02-09 20:59:42', 0, 2, '-1', '-1', 0),
(74, 2, '2023-02-09 21:01:02', '2023-02-09 21:01:02', 0, 6, '-1', '-1', 0),
(75, 2, '2023-02-09 21:01:51', '2023-02-09 21:01:51', 0, 4, '-1', '-1', 0),
(76, 2, '2023-02-09 21:03:11', '2023-02-09 21:03:11', 0, 1003, '-1', '-1', 0),
(77, 2, '2023-02-09 23:15:11', '2023-02-09 23:15:11', 1, 1, '-1', '-1', 1500),
(78, 2, '2023-02-14 18:19:05', '2023-02-09 23:15:22', 1, 1, '-1', '-1', 3700),
(79, 2, '2023-02-09 23:21:52', '2023-02-09 23:16:08', 1, 1004, '-1', '-1', 2400),
(80, 2, '2023-02-09 23:22:41', '2023-02-09 23:22:41', 0, 1004, '-1', '-1', 1000),
(82, 2, '2023-02-11 12:31:01', '2023-02-11 12:30:53', 1, 1005, '-1', '-1', 3000),
(83, 2, '2023-02-11 12:32:18', '2023-02-11 12:32:18', 0, 1005, '-1', '-1', 0),
(84, 2, '2023-02-14 18:26:11', '2023-02-14 18:24:04', 1, 1, '-1', '-1', 3800),
(85, 2, '2023-02-16 19:35:09', '2023-02-14 18:38:50', 1, 1, '-1', '-1', 3600),
(86, 2, '2023-02-16 20:29:44', '2023-02-16 19:35:15', 1, 1, '-1', '-1', 4700),
(88, 2, '2023-02-18 14:15:30', '2023-02-18 14:03:16', 1, 1, '-1', '-1', 3600),
(89, 2, '2023-02-18 14:16:16', '2023-02-18 14:15:44', 0, 1, '-1', '-1', 2400),
(90, 2, '2023-02-28 21:19:05', '2023-02-28 21:18:48', 1, 1006, '-1', '-1', 3400),
(91, 2, '2023-02-28 21:19:42', '2023-02-28 21:19:32', 1, 1006, '-1', '-1', 3000),
(92, 2, '2023-02-28 21:20:16', '2023-02-28 21:20:16', 0, 1006, '-1', '-1', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `idProducto` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `imagen` varchar(150) NOT NULL,
  `precioProducto` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`idProducto`, `nombre`, `imagen`, `precioProducto`) VALUES
(1, 'Hamburguesa', 'https://www.clarin.com/img/2022/05/27/la-hamburguesa-mucho-mas-que___0HXb0UR0v_2000x1500__1.jpg', 500),
(2, 'Pizza', 'https://d320djwtwnl5uo.cloudfront.net/recetas/share/pizza_Mh3H4eanyBKEsStv1YclPWTf9OUqIi.png', 1200),
(3, 'Coca-Cola', 'https://carrefourar.vtexassets.com/arquivos/ids/220177/7790895000997_02.jpg?v=637704294205400000', 300),
(4, 'Milanesa', 'https://upload.wikimedia.org/wikipedia/commons/6/6e/Weekend_in_Buenos_Aires.jpg', 700),
(5, 'Sprite', 'https://carrefourar.vtexassets.com/arquivos/ids/232328/7790895000447_02.jpg?v=637763940887870000', 300);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipopago`
--

CREATE TABLE `tipopago` (
  `idTipoPago` int(20) NOT NULL,
  `metodo` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tipopago`
--

INSERT INTO `tipopago` (`idTipoPago`, `metodo`) VALUES
(1, 'Tarjeta Debito'),
(2, 'Tarjeta Credito'),
(3, 'Transferencia'),
(4, 'QR'),
(5, 'Efectivo');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `idUsuario` int(40) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `direccion` varchar(50) NOT NULL,
  `email` varchar(60) NOT NULL,
  `password` varchar(100) NOT NULL,
  `telefono` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`idUsuario`, `nombre`, `apellido`, `direccion`, `email`, `password`, `telefono`) VALUES
(1, 'Malco', 'Grosso', 'Belgrano 321', 'malco@gmail.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '2664987654'),
(2, 'Martin', 'Perez', 'San Martin 251', 'martin@gmail.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '2664123456'),
(4, 'German', 'Funes', 'Pedernera 678', 'GermanF@gmail.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '2664985632'),
(5, 'Martina', 'Lopez', 'Junin 321', 'MartinaL@gmail.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '2664852369'),
(6, 'Laura', 'Gimenez', 'Las Heras 753', 'LauraG@gmail.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '2664349682'),
(1001, 'z', 'z', 'z', 'zzz@zzz.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '123'),
(1002, 'a', 'a', 'a123', 'aaa@aaa.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '23434'),
(1003, 'x', 'x', 'x', 'xxx@xxx.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '123123'),
(1004, 'y', 'y', 'y', 'y@y.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '5555'),
(1005, 'asd', 'asd', 'sdasd 123', 'yyy@yyy.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '273273'),
(1006, 'mmm', 'mmm', 'mmm', 'mmm@mmm.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', '454545454');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `detallepedido`
--
ALTER TABLE `detallepedido`
  ADD PRIMARY KEY (`idDetallePedido`),
  ADD KEY `idProductoDP` (`idProductoDP`),
  ADD KEY `IdentificadorDetallePedido` (`IdentificadorDetallePedido`);

--
-- Indices de la tabla `empleados`
--
ALTER TABLE `empleados`
  ADD PRIMARY KEY (`idEmpleados`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `idTipoPagoP` (`idTipoPagoP`),
  ADD KEY `idPedidoPago` (`idPedidoPago`);

--
-- Indices de la tabla `pedidos`
--
ALTER TABLE `pedidos`
  ADD PRIMARY KEY (`idPedido`),
  ADD KEY `idUsuarioPedido` (`idUsuarioPedido`),
  ADD KEY `idEmpleadoPedido` (`idEmpleadoPedido`);

--
-- Indices de la tabla `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`idProducto`);

--
-- Indices de la tabla `tipopago`
--
ALTER TABLE `tipopago`
  ADD PRIMARY KEY (`idTipoPago`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `detallepedido`
--
ALTER TABLE `detallepedido`
  MODIFY `idDetallePedido` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=341;

--
-- AUTO_INCREMENT de la tabla `empleados`
--
ALTER TABLE `empleados`
  MODIFY `idEmpleados` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(30) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=57;

--
-- AUTO_INCREMENT de la tabla `pedidos`
--
ALTER TABLE `pedidos`
  MODIFY `idPedido` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=93;

--
-- AUTO_INCREMENT de la tabla `productos`
--
ALTER TABLE `productos`
  MODIFY `idProducto` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `tipopago`
--
ALTER TABLE `tipopago`
  MODIFY `idTipoPago` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `idUsuario` int(40) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1007;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `detallepedido`
--
ALTER TABLE `detallepedido`
  ADD CONSTRAINT `detallepedido_ibfk_1` FOREIGN KEY (`idProductoDP`) REFERENCES `productos` (`idProducto`),
  ADD CONSTRAINT `detallepedido_ibfk_4` FOREIGN KEY (`IdentificadorDetallePedido`) REFERENCES `pedidos` (`idPedido`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`idTipoPagoP`) REFERENCES `tipopago` (`idTipoPago`),
  ADD CONSTRAINT `pago_ibfk_3` FOREIGN KEY (`idPedidoPago`) REFERENCES `pedidos` (`idPedido`);

--
-- Filtros para la tabla `pedidos`
--
ALTER TABLE `pedidos`
  ADD CONSTRAINT `pedidos_ibfk_1` FOREIGN KEY (`idUsuarioPedido`) REFERENCES `usuarios` (`idUsuario`),
  ADD CONSTRAINT `pedidos_ibfk_2` FOREIGN KEY (`idEmpleadoPedido`) REFERENCES `empleados` (`idEmpleados`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
