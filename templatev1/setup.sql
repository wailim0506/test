-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 23, 2024 at 09:52 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT = @@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS = @@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION = @@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `itp4915m_se1d_group4`
--

-- --------------------------------------------------------

--
-- Table structure for table `cart`
--

CREATE TABLE `cart`
(
    `cartID`            char(8) NOT NULL,
    `customerAccountID` char(8) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `cart`
--

INSERT INTO `cart` (`cartID`, `customerAccountID`)
VALUES ('LMSC0001', 'CA00001'),
       ('LMSC0002', 'CA00002'),
       ('LMSC0003', 'CA00003'),
       ('LMSC0004', 'CA00004');

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category`
(
    `categoryID` char(1)     NOT NULL,
    `type`       varchar(17) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`categoryID`, `type`)
VALUES ('A', 'Sheet Metal'),
       ('B', 'Major Assemblies'),
       ('C', 'Light Components'),
       ('D', 'Accessories');

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer`
(
    `customerID`        char(8)     NOT NULL,
    `firstName`         varchar(20) NOT NULL,
    `lastName`          varchar(20) NOT NULL,
    `sex`               char(1)     NOT NULL,
    `emailAddress`      varchar(30) NOT NULL,
    `company`           char(30)    NOT NULL,
    `phoneNumber`       char(11)    NOT NULL,
    `province`          varchar(10) NOT NULL,
    `city`              varchar(10) NOT NULL,
    `companyAddress`    varchar(50) NOT NULL,
    `warehouseAddress`  varchar(50) NOT NULL,
    `joinDate`          date        NOT NULL,
    `paymentMethod`     varchar(10) NOT NULL,
    `imageID`           char(8)     DEFAULT NULL,
    `dateOfBirth`       date        DEFAULT NULL,
    `warehouseAddress2` varchar(50) DEFAULT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`customerID`, `firstName`, `lastName`, `sex`, `emailAddress`, `company`, `phoneNumber`,
                        `province`, `city`, `companyAddress`, `warehouseAddress`, `joinDate`, `paymentMethod`,
                        `imageID`, `dateOfBirth`, `warehouseAddress2`)
VALUES ('LMC00001', 'Peter', 'Zhang', 'F', 'peter.zhang@abc.com', 'AutoTech Solutions', '13012345678', 'Gansu',
        'Jinchang', '23 South Avenue', 'No. 35, Square South Road, Lanzhou, Gansu', '2001-05-23', 'AmericanEx', NULL,
        '2000-02-02', '456 West Street'),
       ('LMC00002', 'Lily', 'Li', 'F', 'lily.li@example.com', 'Legend Motor Limited Company', '13098765432', 'Sichuan',
        'Chengdu', '48/F, Office Tower 2, Chengdu IFS, No.1, Section 3',
        'No. 2, Section 2, Renmin South Road, Wuhou Distric', '2023-08-10', 'MasterCard', NULL, '2024-05-23',
        '123 Main Street'),
       ('LMC00003', 'Michael', 'Wang', 'M', 'michael.wang@example.com', 'Global Auto Spares', '13087654321', 'Beijing',
        'Beijing', '789 East District', 'No. 8, Yongdingmen East Street, Dongcheng District', '2021-11-15', 'Visa',
        NULL, NULL, ''),
       ('LMC00004', 'Wendy', 'Chen', 'F', 'wendy.chen@example.com', 'Premier Motorsupply', '13021654987', 'Shanghai',
        'Shanghai', '1 Pudong New Area', 'No. 301, Zhaojiabang Road, Shanghai', '2022-05-20', 'UnionPay', NULL, NULL,
        '');

-- --------------------------------------------------------

--
-- Table structure for table `customer_account`
--

CREATE TABLE `customer_account`
(
    `customerAccountID` char(8)      NOT NULL,
    `customerID`        char(8)      NOT NULL,
    `isLM`              char(1)      NOT NULL,
    `Status`            varchar(10)  NOT NULL,
    `Password`          varchar(100) NOT NULL,
    `createDate`        date         NOT NULL,
    `pwdChangeDate`     date         NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `customer_account`
--

INSERT INTO `customer_account` (`customerAccountID`, `customerID`, `isLM`, `Status`, `Password`, `createDate`,
                                `pwdChangeDate`)
VALUES ('CA00001', 'LMC00001', 'N', 'active', '$2a$11$NonK/Bctl9a47.zxgGwCseA8b9bD49bnW/Hm2BTXDcWKv1RjD8dY.',
        '2024-05-01', '2024-06-01'),
       ('CA00002', 'LMC00002', 'Y', 'active', '$2a$11$OnjbuMS8T.vMAIlKRAKeieeM1Ta21II5OHtkoZ8xeHWuFCQ2ol92m',
        '2024-05-01', '2024-05-01'),
       ('CA00003', 'LMC00003', 'N', 'active', '$2a$11$xn5O4pbgRs.TSTmef24YFOIgTdfruzNyK9qSN2xMHuISYvG7Lufc6',
        '2024-05-03', '2024-05-07'),
       ('CA00004', 'LMC00004', 'N', 'active', '$2a$11$a1H76k2oFod3SayvEHROHuq9yDFnjKmwjbYaf/sYe/2Ec5T5IDlVe',
        '2024-05-04', '2024-05-07');

-- --------------------------------------------------------

--
-- Table structure for table `customer_dfadd`
--

CREATE TABLE `customer_dfadd`
(
    `customerID` char(8) NOT NULL,
    `dfadd`      int(11) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `customer_dfadd`
--

INSERT INTO `customer_dfadd` (`customerID`, `dfadd`)
VALUES ('LMC00001', 2),
       ('LMC00002', 2),
       ('LMC00003', 1),
       ('LMC00004', 1);

-- --------------------------------------------------------

--
-- Table structure for table `customer_login_history`
--

CREATE TABLE `customer_login_history`
(
    `customerAccountID` char(8)  NOT NULL,
    `loginDate`         datetime NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `customer_login_history`
--

INSERT INTO `customer_login_history` (`customerAccountID`, `loginDate`)
VALUES ('CA00001', '2024-05-29 01:41:34'),
       ('CA00001', '2024-05-29 01:41:44'),
       ('CA00001', '2024-05-29 01:54:29'),
       ('CA00001', '2024-05-29 01:55:20'),
       ('CA00001', '2024-05-29 01:55:56'),
       ('CA00001', '2024-05-29 01:57:29'),
       ('CA00001', '2024-05-29 01:57:59'),
       ('CA00001', '2024-05-29 01:59:12'),
       ('CA00001', '2024-05-29 02:00:08'),
       ('CA00001', '2024-05-29 02:00:18'),
       ('CA00001', '2024-05-29 02:11:11'),
       ('CA00001', '2024-05-29 02:20:01'),
       ('CA00001', '2024-05-29 02:22:16'),
       ('CA00001', '2024-05-29 02:22:32'),
       ('CA00001', '2024-05-29 02:23:01'),
       ('CA00001', '2024-05-29 02:23:10'),
       ('CA00001', '2024-05-29 02:25:17'),
       ('CA00001', '2024-05-29 02:25:20'),
       ('CA00001', '2024-05-29 02:25:25'),
       ('CA00001', '2024-05-30 02:38:04'),
       ('CA00001', '2024-05-30 02:40:23'),
       ('CA00001', '2024-05-30 02:41:33'),
       ('CA00001', '2024-05-30 02:42:21'),
       ('CA00001', '2024-05-30 02:42:26'),
       ('CA00001', '2024-05-30 03:22:16'),
       ('CA00001', '2024-05-30 03:51:31'),
       ('CA00001', '2024-05-30 13:20:47'),
       ('CA00001', '2024-06-02 23:05:50'),
       ('CA00001', '2024-06-02 23:06:13'),
       ('CA00001', '2024-06-02 23:06:50'),
       ('CA00001', '2024-06-02 23:07:52'),
       ('CA00001', '2024-06-03 17:48:12'),
       ('CA00001', '2024-06-03 17:50:20'),
       ('CA00001', '2024-06-03 17:51:45'),
       ('CA00001', '2024-06-03 18:08:41'),
       ('CA00001', '2024-06-03 18:14:00'),
       ('CA00001', '2024-06-03 18:21:00'),
       ('CA00001', '2024-06-03 18:22:19'),
       ('CA00001', '2024-06-03 18:24:03'),
       ('CA00001', '2024-06-03 18:24:50'),
       ('CA00001', '2024-06-03 18:27:01'),
       ('CA00001', '2024-06-03 18:28:59'),
       ('CA00001', '2024-06-03 18:30:08'),
       ('CA00001', '2024-06-03 18:34:51'),
       ('CA00001', '2024-06-03 18:38:45'),
       ('CA00001', '2024-06-03 18:39:01'),
       ('CA00001', '2024-06-03 18:39:03'),
       ('CA00001', '2024-06-03 18:41:08'),
       ('CA00001', '2024-06-03 18:42:13'),
       ('CA00001', '2024-06-03 19:01:54'),
       ('CA00001', '2024-06-03 19:02:54'),
       ('CA00001', '2024-06-03 19:04:15'),
       ('CA00001', '2024-06-03 19:05:08'),
       ('CA00001', '2024-06-03 19:06:13'),
       ('CA00001', '2024-06-03 19:07:15'),
       ('CA00001', '2024-06-03 19:08:56'),
       ('CA00001', '2024-06-03 19:11:11'),
       ('CA00001', '2024-06-03 19:12:01'),
       ('CA00001', '2024-06-03 19:13:00'),
       ('CA00001', '2024-06-03 19:14:20'),
       ('CA00001', '2024-06-03 19:16:30'),
       ('CA00001', '2024-06-03 19:19:34'),
       ('CA00001', '2024-06-03 19:20:07'),
       ('CA00001', '2024-06-03 19:25:37'),
       ('CA00001', '2024-06-03 19:27:44'),
       ('CA00001', '2024-06-03 19:30:40'),
       ('CA00001', '2024-06-03 19:32:31'),
       ('CA00001', '2024-06-03 19:33:25'),
       ('CA00001', '2024-06-03 19:36:17'),
       ('CA00001', '2024-06-03 19:40:14'),
       ('CA00001', '2024-06-03 19:42:48'),
       ('CA00001', '2024-06-03 21:12:35'),
       ('CA00001', '2024-06-03 21:14:17'),
       ('CA00001', '2024-06-03 21:28:12'),
       ('CA00001', '2024-06-03 21:36:16'),
       ('CA00001', '2024-06-03 21:37:31'),
       ('CA00001', '2024-06-03 21:39:08'),
       ('CA00001', '2024-06-03 21:58:19'),
       ('CA00001', '2024-06-04 13:47:00'),
       ('CA00001', '2024-06-04 13:59:21'),
       ('CA00001', '2024-06-04 14:01:25'),
       ('CA00001', '2024-06-09 23:00:50'),
       ('CA00001', '2024-06-09 23:06:24'),
       ('CA00001', '2024-06-10 00:45:57'),
       ('CA00001', '2024-06-10 00:49:51'),
       ('CA00001', '2024-06-10 00:51:43'),
       ('CA00001', '2024-06-10 00:52:00'),
       ('CA00001', '2024-06-12 00:54:24'),
       ('CA00001', '2024-06-12 00:56:33'),
       ('CA00001', '2024-06-12 00:59:08'),
       ('CA00001', '2024-06-12 01:00:05'),
       ('CA00001', '2024-06-20 00:09:04'),
       ('CA00001', '2024-06-20 00:11:57'),
       ('CA00001', '2024-06-20 00:15:20'),
       ('CA00001', '2024-06-20 00:22:22'),
       ('CA00001', '2024-06-20 00:38:45'),
       ('CA00001', '2024-06-20 00:41:39'),
       ('CA00001', '2024-06-20 11:18:31'),
       ('CA00001', '2024-06-20 21:01:09'),
       ('CA00001', '2024-06-20 21:06:19'),
       ('CA00001', '2024-06-20 21:18:57'),
       ('CA00001', '2024-06-20 21:28:28'),
       ('CA00001', '2024-06-20 21:30:17'),
       ('CA00001', '2024-06-20 21:32:53'),
       ('CA00001', '2024-06-20 21:40:21'),
       ('CA00001', '2024-06-20 21:45:23'),
       ('CA00001', '2024-06-20 22:00:08'),
       ('CA00001', '2024-06-20 22:02:38'),
       ('CA00001', '2024-06-20 22:03:52'),
       ('CA00001', '2024-06-20 22:12:24'),
       ('CA00001', '2024-06-20 22:14:23'),
       ('CA00001', '2024-06-20 22:15:25'),
       ('CA00001', '2024-06-20 22:16:05'),
       ('CA00001', '2024-06-20 22:28:08'),
       ('CA00001', '2024-06-20 22:33:18'),
       ('CA00001', '2024-06-20 23:21:34'),
       ('CA00001', '2024-06-20 23:31:20'),
       ('CA00001', '2024-06-20 23:31:42'),
       ('CA00001', '2024-06-20 23:33:45'),
       ('CA00001', '2024-06-20 23:34:46'),
       ('CA00001', '2024-06-20 23:38:11'),
       ('CA00001', '2024-06-20 23:39:49'),
       ('CA00001', '2024-06-20 23:41:21'),
       ('CA00001', '2024-06-20 23:49:38'),
       ('CA00001', '2024-06-20 23:51:01'),
       ('CA00001', '2024-06-20 23:52:10'),
       ('CA00001', '2024-06-20 23:52:44'),
       ('CA00001', '2024-06-20 23:53:51'),
       ('CA00001', '2024-06-21 00:06:23'),
       ('CA00001', '2024-06-21 00:10:00'),
       ('CA00001', '2024-06-21 00:23:00'),
       ('CA00001', '2024-06-21 00:30:18'),
       ('CA00001', '2024-06-21 00:31:14'),
       ('CA00001', '2024-06-21 00:34:50'),
       ('CA00001', '2024-06-21 00:37:20'),
       ('CA00001', '2024-06-21 00:39:09'),
       ('CA00001', '2024-06-21 00:40:24'),
       ('CA00001', '2024-06-21 00:43:34'),
       ('CA00001', '2024-06-21 00:44:06'),
       ('CA00001', '2024-06-21 00:44:54'),
       ('CA00001', '2024-06-21 00:45:13'),
       ('CA00001', '2024-06-21 00:45:38'),
       ('CA00001', '2024-06-21 00:46:34'),
       ('CA00001', '2024-06-21 00:47:59'),
       ('CA00001', '2024-06-21 01:13:42'),
       ('CA00001', '2024-06-21 01:17:10'),
       ('CA00001', '2024-06-21 01:19:42'),
       ('CA00001', '2024-06-21 01:21:45'),
       ('CA00001', '2024-06-21 01:22:33'),
       ('CA00001', '2024-06-21 01:24:09'),
       ('CA00001', '2024-06-21 01:25:15'),
       ('CA00001', '2024-06-21 01:28:22'),
       ('CA00001', '2024-06-21 01:29:13'),
       ('CA00001', '2024-06-21 01:30:19'),
       ('CA00001', '2024-06-21 02:16:52'),
       ('CA00001', '2024-06-21 02:16:58'),
       ('CA00001', '2024-06-22 18:19:10'),
       ('CA00001', '2024-06-22 18:23:36'),
       ('CA00001', '2024-06-22 18:39:50'),
       ('CA00001', '2024-06-22 18:41:36'),
       ('CA00001', '2024-06-22 18:42:52'),
       ('CA00001', '2024-06-22 18:46:51'),
       ('CA00001', '2024-06-22 20:08:45'),
       ('CA00001', '2024-06-22 20:14:11'),
       ('CA00001', '2024-06-22 20:15:28'),
       ('CA00001', '2024-06-22 20:48:06'),
       ('CA00001', '2024-06-22 20:51:41'),
       ('CA00001', '2024-06-23 01:19:45'),
       ('CA00001', '2024-06-23 13:48:06'),
       ('CA00001', '2024-06-23 14:33:53'),
       ('CA00001', '2024-06-23 14:37:31'),
       ('CA00001', '2024-06-23 14:38:22'),
       ('CA00001', '2024-06-23 14:41:33'),
       ('CA00001', '2024-06-23 14:44:12'),
       ('CA00001', '2024-06-23 14:46:23'),
       ('CA00001', '2024-06-23 14:46:30'),
       ('CA00001', '2024-06-23 14:48:00'),
       ('CA00001', '2024-06-23 14:50:04'),
       ('CA00001', '2024-06-23 14:52:39'),
       ('CA00001', '2024-06-23 14:53:40'),
       ('CA00001', '2024-06-23 14:54:10'),
       ('CA00001', '2024-06-23 14:56:17'),
       ('CA00001', '2024-06-23 14:57:00'),
       ('CA00001', '2024-06-23 14:58:11'),
       ('CA00001', '2024-06-23 15:00:11'),
       ('CA00001', '2024-06-23 15:00:55'),
       ('CA00001', '2024-06-23 15:02:40'),
       ('CA00001', '2024-06-23 15:04:09'),
       ('CA00001', '2024-06-23 15:06:14'),
       ('CA00001', '2024-06-23 15:13:28'),
       ('CA00003', '2024-05-29 02:00:48'),
       ('CA00003', '2024-05-29 02:03:19'),
       ('CA00003', '2024-05-29 02:11:23'),
       ('CA00003', '2024-05-29 02:14:41'),
       ('CA00003', '2024-05-29 02:19:54'),
       ('CA00003', '2024-05-29 02:23:17');

-- --------------------------------------------------------

--
-- Table structure for table `deliverman`
--

CREATE TABLE `deliverman`
(
    `delivermanID` char(6) NOT NULL,
    `staffID`      char(8) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `deliverman`
--

INSERT INTO `deliverman` (`delivermanID`, `staffID`)
VALUES ('LMD001', 'LMS00009'),
       ('LMD002', 'LMS00010'),
       ('LMD003', 'LMS00011');

-- --------------------------------------------------------

--
-- Table structure for table `deliveryrelay`
--

CREATE TABLE `deliveryrelay`
(
    `RelayID`   int(11) NOT NULL,
    `RelayName` varchar(255)  DEFAULT NULL,
    `province`  varchar(15)   DEFAULT NULL,
    `city`      varchar(15)   DEFAULT NULL,
    `latitude`  decimal(9, 6) DEFAULT NULL,
    `longitude` decimal(9, 6) DEFAULT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `deliveryrelay`
--

INSERT INTO `deliveryrelay` (`RelayID`, `RelayName`, `province`, `city`, `latitude`, `longitude`)
VALUES (1, 'Window of the World', 'Guangdong', 'Shenzhen', 22.534900, 113.963300),
       (2, 'Baishizhou', 'Guangdong', 'Shenzhen', 22.542600, 113.950800),
       (3, 'Xili', 'Guangdong', 'Shenzhen', 22.550100, 113.939200),
       (4, 'Huobu', 'Guangdong', 'Shenzhen', 22.557700, 113.936500),
       (5, 'Daxin', 'Guangdong', 'Shenzhen', 22.565200, 113.923900),
       (6, 'Taoyuan', 'Guangdong', 'Shenzhen', 22.572800, 113.911300),
       (7, 'Jinganggang', 'Guangdong', 'Shenzhen', 22.580400, 113.898700),
       (8, 'Jinganggang West', 'Guangdong', 'Shenzhen', 22.588000, 113.886100),
       (9, 'Shenzhen Airport East', 'Guangdong', 'Shenzhen', 22.595600, 113.873500),
       (10, 'Shenzhen Airport West', 'Guangdong', 'Shenzhen', 22.603200, 113.860900),
       (11, 'Beijing South Station', 'Beijing', 'Beijing', 39.905000, 116.393300),
       (12, 'Tianjin West Station', 'Tianjin', 'Tianjin', 39.043100, 117.191400),
       (13, 'Shijiazhuang Station', 'Hebei', 'Shijiazhuang', 38.040500, 114.473900),
       (14, 'Taiyuan South Station', 'Shanxi', 'Taiyuan', 37.857500, 112.568300),
       (15, 'Hohhot East Station', 'Inner Mongolia', 'Hohhot', 40.808300, 111.668600),
       (16, 'Shenyang North Station', 'Liaoning', 'Shenyang', 41.813600, 123.433900),
       (17, 'Changchun West Station', 'Jilin', 'Changchun', 43.903000, 125.252200),
       (18, 'Harbin West Station', 'Heilongjiang', 'Harbin', 45.742500, 126.643600),
       (19, 'Shanghai Hongqiao Station', 'Shanghai', 'Shanghai', 31.197500, 121.326400),
       (20, 'Nanjing South Station', 'Jiangsu', 'Nanjing', 32.004400, 118.800800),
       (21, 'Beijing North Station', 'Beijing', 'Beijing', 39.905000, 116.393300);

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE `department`
(
    `deptID` char(5)     NOT NULL,
    `name`   varchar(50) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`deptID`, `name`)
VALUES ('LMD01', 'Sales Office'),
       ('LMD02', 'Purchasing Department'),
       ('LMD03', 'Spares Despatch Department'),
       ('LMD04', 'Goods Inward Department'),
       ('LMD05', 'Store');

-- --------------------------------------------------------

--
-- Table structure for table `discount`
--

CREATE TABLE `discount`
(
    `discountID`    char(10) NOT NULL,
    `percentage`    int(11)  NOT NULL,
    `discountRange` int(11)  NOT NULL,
    `endDate`       date     NOT NULL,
    `createDate`    date     NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `discount`
--

INSERT INTO `discount` (`discountID`, `percentage`, `discountRange`, `endDate`, `createDate`)
VALUES ('DISC0001', 15, 100, '2024-12-31', '2024-01-01'),
       ('DISC0002', 20, 200, '2025-06-30', '2024-03-15'),
       ('DISC0003', 10, 50, '2024-09-30', '2024-06-01'),
       ('DISC0004', 12, 150, '2025-03-31', '2024-09-01'),
       ('DISC0005', 18, 75, '2024-11-30', '2024-05-15'),
       ('DISC0006', 8, 25, '2024-08-31', '2024-02-01');

-- --------------------------------------------------------

--
-- Table structure for table `favourite`
--

CREATE TABLE `favourite`
(
    `customerID` char(8) NOT NULL,
    `itemID`     char(8) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `favourite`
--

INSERT INTO `favourite` (`customerID`, `itemID`)
VALUES ('LMC00001', 'LMP00003'),
       ('LMC00001', 'LMP00006'),
       ('LMC00001', 'LMP00008'),
       ('LMC00001', 'LMP00010'),
       ('LMC00001', 'LMP00013'),
       ('LMC00001', 'LMP00015'),
       ('LMC00001', 'LMP00016'),
       ('LMC00001', 'LMP00020');

-- --------------------------------------------------------

--
-- Table structure for table `feedback`
--

CREATE TABLE `feedback`
(
    `feedbackID`   char(7)       NOT NULL,
    `customerID`   char(8)       NOT NULL,
    `orderID`      char(10) DEFAULT NULL,
    `content`      varchar(1000) NOT NULL,
    `feedbackDate` varchar(30)   NOT NULL,
    `staffID`      char(8)  DEFAULT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `instruction`
--

CREATE TABLE `instruction`
(
    `orderID` char(10)    NOT NULL,
    `content` varchar(60) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `invoice`
--

CREATE TABLE `invoice`
(
    `customerAccountID` char(8)  NOT NULL,
    `orderID`           char(10) NOT NULL,
    `invoiceNumber`     char(10) NOT NULL,
    `status`            varchar(20) DEFAULT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `invoice`
--

INSERT INTO `invoice` (`customerAccountID`, `orderID`, `invoiceNumber`, `status`)
VALUES ('CA00001', 'OD24020001', 'IN00001', 'confirmed'),
       ('CA00001', 'OD24050002', 'IN00002', 'confirmed'),
       ('CA00001', 'OD24060001', 'IN00003', NULL),
       ('CA00001', 'OD24060002', 'IN00004', NULL),
       ('CA00001', 'OD24060003', 'IN00005', NULL),
       ('CA00001', 'OD24060004', 'IN00006', NULL),
       ('CA00001', 'OD24060005', 'IN00007', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `location`
--

CREATE TABLE `location`
(
    `province` varchar(15) NOT NULL,
    `city`     varchar(15) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `location`
--

INSERT INTO `location` (`province`, `city`)
VALUES ('Anhui', 'Anqing'),
       ('Anhui', 'Bengbu'),
       ('Anhui', 'Bozhou'),
       ('Anhui', 'Chuzhou'),
       ('Anhui', 'Fuyang'),
       ('Anhui', 'Hefei'),
       ('Anhui', 'Huainan'),
       ('Anhui', 'Ma\'anshan'),
       ('Anhui', 'Tongling'),
       ('Anhui', 'Wuhu'),
       ('Beijing', 'Beijing'),
       ('Fujian', 'Fuzhou'),
       ('Fujian', 'Longyan'),
       ('Fujian', 'Nanping'),
       ('Fujian', 'Putian'),
       ('Fujian', 'Quanzhou'),
       ('Fujian', 'Sanming'),
       ('Fujian', 'Xiamen'),
       ('Fujian', 'Zhangzhou'),
       ('Gansu', 'Dingxi'),
       ('Gansu', 'Dunhuang'),
       ('Gansu', 'Jiayuguan'),
       ('Gansu', 'Jinchang'),
       ('Gansu', 'Lanzhou'),
       ('Gansu', 'Pingliang'),
       ('Gansu', 'Tianshui'),
       ('Gansu', 'Wuwei'),
       ('Gansu', 'Zhangye'),
       ('Guangdong', 'Dongguan'),
       ('Guangdong', 'Foshan'),
       ('Guangdong', 'Guangzhou'),
       ('Guangdong', 'Huizhou'),
       ('Guangdong', 'Jiangmen'),
       ('Guangdong', 'Maoming'),
       ('Guangdong', 'Shantou'),
       ('Guangdong', 'Shanwei'),
       ('Guangdong', 'Shenzhen'),
       ('Guangdong', 'Zhanjiang'),
       ('Guangdong', 'Zhongshan'),
       ('Guangdong', 'Zhuhai'),
       ('Guizhou', 'Anshun'),
       ('Guizhou', 'Bijie'),
       ('Guizhou', 'Guiyang'),
       ('Guizhou', 'Liupanshui'),
       ('Guizhou', 'Tongren'),
       ('Guizhou', 'Zunyi'),
       ('Hainan', 'Danzhou'),
       ('Hainan', 'Haikou'),
       ('Hainan', 'Sanya'),
       ('Hainan', 'Wenchang'),
       ('Hainan', 'Wuzhishan'),
       ('Hebei', 'Baoding'),
       ('Hebei', 'Cangzhou'),
       ('Hebei', 'Chengde'),
       ('Hebei', 'Handan'),
       ('Hebei', 'Langfang'),
       ('Hebei', 'Qinhuangdao'),
       ('Hebei', 'Shijiazhuang'),
       ('Hebei', 'Tangshan'),
       ('Hebei', 'Zhangjiakou'),
       ('Heilongjiang', 'Daqing'),
       ('Heilongjiang', 'Harbin'),
       ('Heilongjiang', 'Heihe'),
       ('Heilongjiang', 'Jiamusi'),
       ('Heilongjiang', 'Mudanjiang'),
       ('Heilongjiang', 'Qiqihar'),
       ('Heilongjiang', 'Suihua'),
       ('Henan', 'Anyang'),
       ('Henan', 'Jiaozuo'),
       ('Henan', 'Kaifeng'),
       ('Henan', 'Luoyang'),
       ('Henan', 'Pingdingshan'),
       ('Henan', 'Puyang'),
       ('Henan', 'Sanmenxia'),
       ('Henan', 'Xinxiang'),
       ('Henan', 'Xuchang'),
       ('Henan', 'Zhengzhou'),
       ('Hubei', 'Ezhou'),
       ('Hubei', 'Huanggang'),
       ('Hubei', 'Jingzhou'),
       ('Hubei', 'Shiyan'),
       ('Hubei', 'Wuhan'),
       ('Hubei', 'Xiangyang'),
       ('Hubei', 'Xiaogan'),
       ('Hubei', 'Yichang'),
       ('Hunan', 'Changde'),
       ('Hunan', 'Changsha'),
       ('Hunan', 'Chenzhou'),
       ('Hunan', 'Hengyang'),
       ('Hunan', 'Loudi'),
       ('Hunan', 'Xiangtan'),
       ('Hunan', 'Yiyang'),
       ('Hunan', 'Yueyang'),
       ('Hunan', 'Zhuzhou'),
       ('Jiangsu', 'Changzhou'),
       ('Jiangsu', 'Huai\'an'),
       ('Jiangsu', 'Lianyungang'),
       ('Jiangsu', 'Nanjing'),
       ('Jiangsu', 'Nantong'),
       ('Jiangsu', 'Suzhou'),
       ('Jiangsu', 'Taizhou'),
       ('Jiangsu', 'Wuxi'),
       ('Jiangsu', 'Yancheng'),
       ('Jiangsu', 'Yangzhou'),
       ('Jiangxi', 'Fuzhou'),
       ('Jiangxi', 'Ganzhou'),
       ('Jiangxi', 'Jingdezhen'),
       ('Jiangxi', 'Jiujiang'),
       ('Jiangxi', 'Nanchang'),
       ('Jiangxi', 'Shangrao'),
       ('Jiangxi', 'Xinyu'),
       ('Jiangxi', 'Yingtan'),
       ('Jilin', 'Changchun'),
       ('Jilin', 'Jilin'),
       ('Jilin', 'Liaoyuan'),
       ('Jilin', 'Siping'),
       ('Jilin', 'Songyuan'),
       ('Jilin', 'Yanbian'),
       ('Liaoning', 'Anshan'),
       ('Liaoning', 'Benxi'),
       ('Liaoning', 'Dalian'),
       ('Liaoning', 'Dandong'),
       ('Liaoning', 'Fushun'),
       ('Liaoning', 'Fuxin'),
       ('Liaoning', 'Jinzhou'),
       ('Liaoning', 'Liaoyang'),
       ('Liaoning', 'Shenyang'),
       ('Liaoning', 'Yingkou'),
       ('Qinghai', 'Golmud'),
       ('Qinghai', 'Haibei'),
       ('Qinghai', 'Haidong'),
       ('Qinghai', 'Xining'),
       ('Qinghai', 'Yushu'),
       ('Shaanxi', 'Ankang'),
       ('Shaanxi', 'Baoji'),
       ('Shaanxi', 'Hanzhong'),
       ('Shaanxi', 'Shangluo'),
       ('Shaanxi', 'Tongchuan'),
       ('Shaanxi', 'Weinan'),
       ('Shaanxi', 'Xi\'an'),
       ('Shaanxi', 'Xianyang'),
       ('Shaanxi', 'Yan\'an'),
       ('Shandong', 'Dongying'),
       ('Shandong', 'Jinan'),
       ('Shandong', 'Jining'),
       ('Shandong', 'Linyi'),
       ('Shandong', 'Qingdao'),
       ('Shandong', 'Taian'),
       ('Shandong', 'Weifang'),
       ('Shandong', 'Yantai'),
       ('Shandong', 'Zaozhuang'),
       ('Shandong', 'Zibo'),
       ('Shanghai', 'Shanghai'),
       ('Shanxi', 'Changzhi'),
       ('Shanxi', 'Datong'),
       ('Shanxi', 'Jincheng'),
       ('Shanxi', 'Linfen'),
       ('Shanxi', 'Shuozhou'),
       ('Shanxi', 'Taiyuan'),
       ('Shanxi', 'Xinzhou'),
       ('Shanxi', 'Yuncheng'),
       ('Sichuan', 'Chengdu'),
       ('Sichuan', 'Chongqing'),
       ('Sichuan', 'Deyang'),
       ('Sichuan', 'Leshan'),
       ('Sichuan', 'Luzhou'),
       ('Sichuan', 'Mianyang'),
       ('Sichuan', 'Neijiang'),
       ('Sichuan', 'Panzhihua'),
       ('Sichuan', 'Suining'),
       ('Sichuan', 'Zigong'),
       ('Yunnan', 'Baoshan'),
       ('Yunnan', 'Dali'),
       ('Yunnan', 'Kunming'),
       ('Yunnan', 'Lijiang'),
       ('Yunnan', 'Lincang'),
       ('Yunnan', 'Qujing'),
       ('Yunnan', 'Xishuangbanna'),
       ('Yunnan', 'Yuxi'),
       ('Yunnan', 'Zhaotong'),
       ('Zhejiang', 'Hangzhou'),
       ('Zhejiang', 'Huzhou'),
       ('Zhejiang', 'Jiaxing'),
       ('Zhejiang', 'Jinhua'),
       ('Zhejiang', 'Ningbo'),
       ('Zhejiang', 'Quzhou'),
       ('Zhejiang', 'Shaoxing'),
       ('Zhejiang', 'Taizhou'),
       ('Zhejiang', 'Wenzhou'),
       ('Zhejiang', 'Zhoushan');

-- --------------------------------------------------------

--
-- Table structure for table `order_`
--

CREATE TABLE `order_`
(
    `orderID`           char(10)    NOT NULL,
    `customerAccountID` char(8)     NOT NULL,
    `staffAccountID`    char(8)     NOT NULL,
    `orderSerialNumber` char(10)    NOT NULL,
    `orderDate`         date        NOT NULL,
    `discountID`        char(10)    DEFAULT NULL,
    `status`            varchar(20) NOT NULL,
    `comment`           varchar(60) DEFAULT NULL,
    `feedbackID`        char(7)     DEFAULT NULL,
    `DeliveryRelayID`   int(10)     DEFAULT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `order_`
--

INSERT INTO `order_` (`orderID`, `customerAccountID`, `staffAccountID`, `orderSerialNumber`, `orderDate`, `discountID`,
                      `status`, `comment`, `feedbackID`, `DeliveryRelayID`)
VALUES ('OD24020001', 'CA00001', 'SA00006', 'SN24020001', '2024-02-11', NULL, 'Shipped', NULL, NULL, 9),
       ('OD24050002', 'CA00001', 'SA00006', 'SN24050002', '2024-05-15', NULL, 'Shipped', NULL, NULL, 9),
       ('OD24060001', 'CA00001', 'SA00006', 'SN24060001', '2024-06-01', NULL, 'Ready to Ship', NULL, NULL, 9),
       ('OD24060002', 'CA00001', 'SA00006', 'SN24060002', '2024-06-05', NULL, 'Ready to Ship', NULL, NULL, 9),
       ('OD24060003', 'CA00001', 'SA00006', 'SN24060003', '2024-06-06', NULL, 'Processing', NULL, NULL, 8),
       ('OD24060004', 'CA00001', 'SA00006', 'SN24060004', '2024-06-12', NULL, 'Processing', NULL, NULL, 8),
       ('OD24060005', 'CA00001', 'SA00006', 'SN24060005', '2024-06-12', NULL, 'Pending', NULL, NULL, 5);

-- --------------------------------------------------------

--
-- Table structure for table `order_line`
--

CREATE TABLE `order_line`
(
    `partNumber`     char(6)  NOT NULL,
    `orderID`        char(10) NOT NULL,
    `quantity`       int(11)  NOT NULL,
    `orderUnitPrice` int(11)  NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `order_line`
--

INSERT INTO `order_line` (`partNumber`, `orderID`, `quantity`, `orderUnitPrice`)
VALUES ('A00001', 'OD24060004', 1, 500),
       ('A00002', 'OD24060003', 20, 500),
       ('A00002', 'OD24060004', 1, 800),
       ('A00003', 'OD24020001', 5, 950),
       ('A00003', 'OD24050002', 5, 400),
       ('A00003', 'OD24060004', 1, 1000),
       ('A00004', 'OD24060001', 10, 243),
       ('A00005', 'OD24060002', 40, 400),
       ('B00001', 'OD24020001', 50, 500),
       ('B00001', 'OD24060001', 4, 300),
       ('B00001', 'OD24060002', 10, 500),
       ('B00002', 'OD24060002', 3, 1000),
       ('B00003', 'OD24060002', 24, 260),
       ('C00002', 'OD24020001', 50, 0),
       ('C00003', 'OD24060002', 30, 460),
       ('C00004', 'OD24060002', 6, 500),
       ('C00004', 'OD24060005', 1, 300),
       ('C00005', 'OD24060001', 1, 200),
       ('D00002', 'OD24060002', 20, 30),
       ('D00005', 'OD24060002', 20, 100);

-- --------------------------------------------------------

--
-- Table structure for table `permission`
--

CREATE TABLE `permission`
(
    `permissionID` char(4)     NOT NULL,
    `Name`         varchar(30) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `permission`
--

INSERT INTO `permission` (`permissionID`, `Name`)
VALUES ('MP01', 'Sales manager'),
       ('MP02', 'Storeman'),
       ('MP03', 'Department manager'),
       ('MP04', 'Delivery man'),
       ('MP05', 'Purchasing Manager'),
       ('MP06', 'Order Processing Clerk');

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product`
(
    `itemID`       char(8)      NOT NULL,
    `category`     char(1)      NOT NULL,
    `partNumber`   char(6)      NOT NULL,
    `onSaleQty`    int(11)      NOT NULL,
    `LM_onSaleQty` int(11)      NOT NULL,
    `description`  varchar(500) NOT NULL,
    `price`        int(11)      NOT NULL,
    `imageID`      char(8) DEFAULT NULL,
    `multiMediaID` char(8) DEFAULT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `product`
--

INSERT INTO `product` (`itemID`, `category`, `partNumber`, `onSaleQty`, `LM_onSaleQty`, `description`, `price`,
                       `imageID`, `multiMediaID`)
VALUES ('LMP00001', 'A', 'A00001', 297, 500,
        'Elevate the appearance of your car with our premium tinned plate cover. Crafted from high-quality metals, this cover adds a touch of elegance and a polished, reflective finish to your vehicle exterior. Designed for a seamless fit, it enhances the overall aesthetics while protecting your car surface from minor scratches and weathering.',
        500, NULL, NULL),
       ('LMP00002', 'A', 'A00002', 497, 700,
        'Enhance the style and protection of your car doors with our custom door covers. Crafted from durable materials, these covers seamlessly integrate with your vehicle design, offering a sleek and modern appearance. Safeguard your doors from scratches, dings, and environmental elements, while adding a personalized touch to your car exterior.',
        800, NULL, NULL),
       ('LMP00003', 'A', 'A00003', 197, 300,
        'Enhance the style and protection of your car doors with our custom door covers. Crafted from durable materials, these covers seamlessly integrate with your vehicle design, offering a sleek and modern appearance. Safeguard your doors from scratches, dings, and environmental elements, while adding a personalized touch to your car exterior.',
        1000, NULL, NULL),
       ('LMP00004', 'A', 'A00004', 299, 400,
        'Enhance your car look with our stylish and protective hood cover. Crafted from durable materials, it offers a custom fit to shield your vehicle hood from weather and minor damage, elevating your driving experience.',
        360, NULL, NULL),
       ('LMP00005', 'A', 'A00005', 249, 400,
        'Our premium trunk lid adds a sleek, modern touch to your car. Crafted from high-quality materials, it seamlessly integrates with your vehicle design, offering a custom fit and enhanced protection for your trunk area. Elevate your car style with this versatile and functional accessory.',
        214, NULL, NULL),
       ('LMP00006', 'B', 'B00001', 849, 1000,
        'Optimize your car engine cooling with our advanced cooling system cover. Designed to improve airflow and heat dissipation, this cover ensures efficient engine operation, reducing the risk of overheating and prolonging the lifespan of your vehicle vital components.',
        440, NULL, NULL),
       ('LMP00007', 'B', 'B00002', 999, 1500,
        'Unveil the power within your car with our engine disassembly cover. This transparent cover allows you to see the intricate workings of your engine, perfect for enthusiasts and mechanics. Crafted with durable materials, it provides a clear view while protecting the engine components during maintenance and repairs.',
        700, NULL, NULL),
       ('LMP00008', 'B', 'B00003', 430, 600,
        'Enhance the performance of your car with our precision-engineered gear cover. Crafted from high-strength materials, this cover safeguards your gearbox components from wear, tear, and environmental factors, ensuring smooth and efficient power transmission. Elevate your driving experience with this functional and durable accessory.',
        500, NULL, NULL),
       ('LMP00009', 'B', 'B00004', 430, 700,
        'Showcase the braking power of your car with our stylish brake caliper covers. Designed to fit over your vehicle brake calipers, these covers not only enhance the visual appeal but also help protect the calipers from the elements. Available in a range of colors, they add a personalized touch to your ride.',
        560, NULL, NULL),
       ('LMP00010', 'B', 'B00005', 500, 900,
        'Protect the heart of your car air suspension system with our rugged compressor cover. Crafted from durable materials, this cover shields your air suspension compressor from dust, debris, and environmental factors, ensuring reliable performance and extended lifespan. Elevate your driving experience with this functional and discreet accessory.',
        145, NULL, NULL),
       ('LMP00011', 'C', 'C00001', 320, 500,
        'Upgrade the lighting of your car with our premium silver halogen bulbs. Designed to provide a brighter, whiter beam of light, these bulbs offer enhanced visibility and a sleek, modern appearance. Crafted for easy installation, they are a simple yet impactful way to elevate the driving experience.',
        340, NULL, NULL),
       ('LMP00012', 'C', 'C00002', 230, 800,
        'Enhance the style of your car with our sleek tail light covers. Crafted from high-quality materials, these covers seamlessly integrate with your vehicle existing tail lights, adding a touch of personalization and a modern aesthetic. Protect your tail lights from scratches and weathering while elevating your car overall appearance.',
        650, NULL, NULL),
       ('LMP00013', 'C', 'C00003', 500, 400,
        'Elevate the front-end design of your car with our stylish headlight covers. Crafted from durable, scratch-resistant materials, these covers fit snugly over your existing headlights, providing a custom, integrated look. Protect your headlights from road debris and environmental factors while adding a modern, eye-catching accent to your vehicle.',
        200, NULL, NULL),
       ('LMP00014', 'C', 'C00004', 499, 600,
        'Elevate the front-end design of your car with our stylish headlight covers. Crafted from durable, scratch-resistant materials, these covers fit snugly over your existing headlights, providing a custom, integrated look. Protect your headlights from road debris and environmental factors while adding a modern, eye-catching accent to your vehicle.',
        300, NULL, NULL),
       ('LMP00015', 'C', 'C00005', 460, 500,
        'Upgrade the front-end style of your car with our sleek headlight covers. Crafted from durable, scratch-resistant materials, these covers seamlessly integrate with your existing headlights, providing a custom, integrated look. Protect your headlights from road debris and environmental factors while adding a modern, eye-catching accent to your vehicle.',
        100, NULL, NULL),
       ('LMP00016', 'D', 'D00001', 299, 400,
        'Protect the integrity of your car tire pressure monitoring system with our specialized cover. Crafted from rugged materials, this cover shields the sensor from debris, weather, and damage, ensuring the accurate and reliable performance of your TPMS. Keep your tires at optimal pressure and enhance the safety of your driving experience.',
        340, NULL, NULL),
       ('LMP00017', 'D', 'D00002', 499, 800,
        'Charge your devices on-the-go with our versatile USB car charger cover. Designed to seamlessly integrate with your vehicle power outlet, this cover protects the charger from dust and damage while providing multiple USB ports for simultaneous charging. Keep your devices powered up and your car interior looking sleek and organized.',
        800, NULL, NULL),
       ('LMP00018', 'D', 'D00003', 240, 400,
        'Safeguard your car pristine condition with our premium car cover. Crafted from durable, weatherproof materials, this cover shields your vehicle from the elements, protecting it from dirt, debris, and UV damage. Designed for a custom fit, it ensures your car remains in showroom-ready condition, whether in storage or parked outdoors.',
        455, NULL, NULL),
       ('LMP00019', 'D', 'D00004', 500, 600,
        'Charge your devices with ease using our car vent charger cover. This sleek and functional accessory seamlessly integrates with your vehicle air vents, providing a secure and convenient USB charging solution. Crafted from high-quality materials, it protects your charger from dust and damage, keeping your car interior organized and clutter-free.',
        200, NULL, NULL),
       ('LMP00020', 'D', 'D00005', 600, 700,
        'With our adaptable car phone holder cover, you can keep your smartphone close at hand while driving. With its stylish design that blends in well with the dashboard or air vents in your car, this cover safely holds your device in place so you can use it hands-free and with ease. Because it is made of sturdy materials, it shields your phone from external influences and vibrations.',
        500, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `product_in_cart`
--

CREATE TABLE `product_in_cart`
(
    `cartID`     char(8)     NOT NULL,
    `itemID`     char(8)     NOT NULL,
    `quantity`   int(11)     NOT NULL,
    `inCartDate` varchar(30) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `product_in_cart`
--

INSERT INTO `product_in_cart` (`cartID`, `itemID`, `quantity`, `inCartDate`)
VALUES ('LMSC0001', 'LMP00001', 2, '22/06/2024'),
       ('LMSC0001', 'LMP00002', 2, '22/06/2024'),
       ('LMSC0001', 'LMP00003', 2, '22/06/2024'),
       ('LMSC0001', 'LMP00004', 1, '22/06/2024'),
       ('LMSC0001', 'LMP00005', 1, '22/06/2024'),
       ('LMSC0001', 'LMP00006', 1, '22/06/2024'),
       ('LMSC0001', 'LMP00007', 1, '22/06/2024'),
       ('LMSC0001', 'LMP00014', 1, '22/06/2024'),
       ('LMSC0001', 'LMP00016', 1, '22/06/2024'),
       ('LMSC0001', 'LMP00017', 1, '22/06/2024');

-- --------------------------------------------------------

--
-- Table structure for table `resource`
--

CREATE TABLE `resource`
(
    `id`   int(11) NOT NULL,
    `name` varchar(255) DEFAULT NULL,
    `type` varchar(255) DEFAULT NULL,
    `path` varchar(255) DEFAULT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Stand-in structure for view `shippedordertotals`
-- (See below for the actual view)
--
CREATE TABLE `shippedordertotals`
(
    `orderID`           char(10),
    `CustomerAccountID` char(8),
    `CustomerID`        char(8),
    `CustomerName`      varchar(41),
    `orderDate`         date,
    `OrderStatus`       varchar(20),
    `orderTotal`        decimal(56, 2)
);

-- --------------------------------------------------------

--
-- Table structure for table `shipping_detail`
--

CREATE TABLE `shipping_detail`
(
    `orderID`         char(10)    NOT NULL,
    `delivermanID`    char(6)     NOT NULL,
    `shippingDate`    date        NOT NULL,
    `remark`          varchar(30) DEFAULT NULL,
    `expressNumber`   varchar(25) DEFAULT NULL,
    `shippingAddress` varchar(80) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `shipping_detail`
--

INSERT INTO `shipping_detail` (`orderID`, `delivermanID`, `shippingDate`, `remark`, `expressNumber`, `shippingAddress`)
VALUES ('OD24020001', 'LMD001', '2024-03-01', NULL, 'EN0001', 'No. 8, Yongdingmen East Street, Dongcheng District'),
       ('OD24050002', 'LMD003', '2024-05-08', NULL, 'EN0013', 'No. 8, Yongdingmen East Street, Dongcheng District'),
       ('OD24060001', 'LMD001', '2024-07-13', NULL, NULL, 'No. 8, Yongdingmen East Street, Dongcheng District'),
       ('OD24060002', 'LMD001', '2024-07-15', NULL, NULL, 'No. 8, Yongdingmen East Street, Dongcheng District'),
       ('OD24060003', 'LMD001', '2024-07-30', NULL, NULL, 'No. 8, Yongdingmen East Street, Dongcheng District'),
       ('OD24060004', 'LMD001', '2024-07-31', NULL, NULL, 'No. 8, Yongdingmen East Street, Dongcheng District'),
       ('OD24060005', 'LMD003', '2024-07-31', NULL, NULL, 'No. 8, Yongdingmen East Street, Dongcheng District');

-- --------------------------------------------------------

--
-- Table structure for table `spare_part`
--

CREATE TABLE `spare_part`
(
    `partNumber`   char(6)     NOT NULL,
    `supplierID`   char(10)    NOT NULL,
    `categoryID`   char(1)     NOT NULL,
    `name`         varchar(50) NOT NULL,
    `reorderLevel` int(11)     NOT NULL,
    `dangerLevel`  int(11)     NOT NULL,
    `quantity`     int(11)     NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `spare_part`
--

INSERT INTO `spare_part` (`partNumber`, `supplierID`, `categoryID`, `name`, `reorderLevel`, `dangerLevel`, `quantity`)
VALUES ('A00001', 'SIDIN00001', 'A', 'Tinned Plate', 100, 50, 799),
       ('A00002', 'SIDJP00001', 'A', 'Car Door', 70, 30, 1199),
       ('A00003', 'SIDUK00001', 'A', 'Premium Car Door', 200, 130, 499),
       ('A00004', 'SIDUK00002', 'A', 'Hood Cover', 400, 200, 700),
       ('A00005', 'SIDCN00001', 'A', 'Trunk Lid', 100, 50, 650),
       ('B00001', 'SIDUK00002', 'B', 'Engine Cooling', 70, 30, 1850),
       ('B00002', 'SIDKR00001', 'B', 'Car Engine Disassembled', 320, 210, 2500),
       ('B00003', 'SIDJP00001', 'B', 'Gear', 250, 100, 1030),
       ('B00004', 'SIDUS00002', 'B', 'Brake Calipers', 200, 100, 1130),
       ('B00005', 'SIDUS00001', 'B', 'Air Suspension Compressor', 100, 30, 1400),
       ('C00001', 'SIDKR00001', 'C', 'Silver Light Halogen Bulbs', 200, 50, 820),
       ('C00002', 'SIDBR00001', 'C', 'Tail Light', 200, 130, 1030),
       ('C00003', 'SIDCN00001', 'C', 'Headlight', 200, 130, 900),
       ('C00004', 'SIDUS00001', 'C', 'Interior Dome Light', 400, 200, 1100),
       ('C00005', 'SIDIN00001', 'C', 'Front Headlight', 100, 50, 960),
       ('D00001', 'SIDJP00001', 'D', 'Tire Pressure Monitor', 70, 30, 700),
       ('D00002', 'SIDCN00001', 'D', 'USB Car Charger', 200, 130, 1300),
       ('D00003', 'SIDUK00003', 'D', 'Car Cover', 100, 50, 640),
       ('D00004', 'SIDUK00003', 'D', 'Car Vent Charger', 100, 50, 1100),
       ('D00005', 'SIDCN00001', 'D', 'Phone Holder', 400, 200, 1300);

-- --------------------------------------------------------

--
-- Table structure for table `staff`
--

CREATE TABLE `staff`
(
    `staffID`      char(8)     NOT NULL,
    `deptID`       char(5)     NOT NULL,
    `firstName`    varchar(20) NOT NULL,
    `lastName`     varchar(20) NOT NULL,
    `sex`          char(1)     NOT NULL,
    `emailAddress` varchar(30) NOT NULL,
    `phoneNumber`  char(11)    NOT NULL,
    `dateOfBirth`  date        NOT NULL,
    `jobTitle`     varchar(40) NOT NULL,
    `delivermanID` char(6) DEFAULT NULL,
    `imageID`      char(8) DEFAULT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `staff`
--

INSERT INTO `staff` (`staffID`, `deptID`, `firstName`, `lastName`, `sex`, `emailAddress`, `phoneNumber`, `dateOfBirth`,
                     `jobTitle`, `delivermanID`, `imageID`)
VALUES ('LMS00001', 'LMD01', 'Siu', 'shuan', 'F', 'siu.leung@example.com', '12376543210', '1980-05-15', 'Sales Manager',
        NULL, NULL),
       ('LMS00002', 'LMD03', 'Mei', 'Wong', 'F', 'mei.wong@example.com', '09876543210', '1985-09-20',
        'Order Processing Clerk', NULL, NULL),
       ('LMS00003', 'LMD02', 'Wai', 'Chan', 'M', 'wai.chan@example.com', '02468013579', '1978-02-28',
        'Purchasing Staff', NULL, NULL),
       ('LMS00004', 'LMD04', 'Ling', 'Lam', 'F', 'ling.lam@example.com', '13579024680', '1990-11-05',
        'Goods Inward Staff', NULL, NULL),
       ('LMS00005', 'LMD05', 'Kin', 'Yuen', 'M', 'kin.yuen@example.com', '08642013579', '1975-06-30', 'Storeman', NULL,
        NULL),
       ('LMS00006', 'LMD03', 'Lily', 'Li', 'F', 'lily.li@example.com', '09876543211', '1982-03-15',
        'Order Processing Clerk', NULL, NULL),
       ('LMS00007', 'LMD03', 'Fiona', 'Chen', 'F', 'fiona.chen@example.com', '09876543212', '1990-07-28',
        'Order Processing Clerk', NULL, NULL),
       ('LMS00008', 'LMD03', 'Michael', 'Wu', 'M', 'michael.wu@example.com', '09876543213', '1988-11-02',
        'Order Processing Clerk', NULL, NULL),
       ('LMS00009', 'LMD03', 'Liang', 'Chen', 'M', 'LC@example.com', '13812345678', '1998-12-04', 'Deliverman',
        'LMD001', NULL),
       ('LMS00010', 'LMD03', 'Mei', 'Wang', 'F', 'MW@example.com', '15987654321', '1993-07-24', 'Deliverman', 'LMD002',
        NULL),
       ('LMS00011', 'LMD03', 'Feng', 'Zhang', 'M', 'FZ@example.com', '1861234567', '1992-10-16', 'Deliverman', 'LMD003',
        NULL);

-- --------------------------------------------------------

--
-- Table structure for table `staff_account`
--

CREATE TABLE `staff_account`
(
    `staffAccountID` char(8)      NOT NULL,
    `staffID`        char(8)      NOT NULL,
    `status`         varchar(10)  NOT NULL,
    `password`       varchar(100) NOT NULL,
    `createDate`     date         NOT NULL,
    `pwdChangeDate`  date         NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `staff_account`
--

INSERT INTO `staff_account` (`staffAccountID`, `staffID`, `status`, `password`, `createDate`, `pwdChangeDate`)
VALUES ('SA00001', 'LMS00001', 'active', '$2a$11$Lm4yIPKczoUpgmGNOqwxdetvm3Ei9nxaHJpz/EZakHSC21BtkQd7y', '2024-05-01',
        '2024-06-03'),
       ('SA00002', 'LMS00002', 'active', '$2a$11$ROw0TLKZTVkyjIo9no3UX.4xVlsyNy4B4aUiHCD76XVCcQhFchEkq', '2024-05-02',
        '2024-05-13'),
       ('SA00003', 'LMS00003', 'active', '$2a$11$w2rx1YUxYjo9CvNoGABiJe04JVE55d3otrzOyZoB.BsG26cLukS2O', '2024-05-10',
        '2024-05-13'),
       ('SA00004', 'LMS00004', 'active', '$2a$11$cBnH8E/j7jLzc/6IlzrNZu3LoFK2sGnv1vdSkmbupH5i5dMzitpMG', '2024-05-11',
        '2024-05-02'),
       ('SA00005', 'LMS00005', 'active', '$2a$11$YAwMphVbJYlIdllgZ3k1x.xRm7rkRhoNnJvqrnKenO.DAZesAtfrW', '2024-05-15',
        '2024-05-10'),
       ('SA00006', 'LMS00006', 'active', '$2a$11$7FgtPouKfa86CTPpaxg3vekIG9joziut1WBIl20MGpv1shzp.NOTa', '2024-06-04',
        '2024-06-16'),
       ('SA00007', 'LMS00007', 'active', '$2a$11$jShG/jYvl1Bk3YXMjskzXONmRqFFpjM9OYzsRPpQpzB9ziLDrb6Q.', '2024-06-03',
        '2024-06-17'),
       ('SA00008', 'LMS00008', 'active', '$2a$11$o4InuCLpWHC4UqqDxqr3xuws2J7mhFkcLW076ZXzpOJkoB0P8sCQO', '2024-06-04',
        '2024-06-04'),
       ('SA00009', 'LMS00009', 'active', '$2a$11$ymoTOnGOpa4ecldl5b0DyeHmr7Gan2yS4YBeEwMWaEW3gkiI8JSii', '2024-06-01',
        '2024-06-01'),
       ('SA00010', 'LMS00010', 'active', '$2a$11$8rST.7FvIsNHVTvn1r1x0uIbwanvzGVGiDGGeG1dWxmTMXpRDWbty', '2024-06-03',
        '2024-06-03'),
       ('SA00011', 'LMS00011', 'active', '$2a$11$CKaxvDzZSq9bt46RL7sOJ.wYgy5PrcKRoH7CN91d1v.8OBUd7pOya', '2024-06-04',
        '2024-06-04');

-- --------------------------------------------------------

--
-- Table structure for table `staff_account_permission`
--

CREATE TABLE `staff_account_permission`
(
    `staffAccountID` char(8) NOT NULL,
    `permissionID`   char(4) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `staff_account_permission`
--

INSERT INTO `staff_account_permission` (`staffAccountID`, `permissionID`)
VALUES ('SA00001', 'MP01'),
       ('SA00002', 'MP01'),
       ('SA00003', 'MP02'),
       ('SA00004', 'MP03'),
       ('SA00005', 'MP04'),
       ('SA00006', 'MP04'),
       ('SA00007', 'MP02'),
       ('SA00008', 'MP05'),
       ('SA00009', 'MP05'),
       ('SA00010', 'MP02'),
       ('SA00011', 'MP03');

-- --------------------------------------------------------

--
-- Table structure for table `staff_login_history`
--

CREATE TABLE `staff_login_history`
(
    `staffAccountID` char(8)  NOT NULL,
    `loginDate`      datetime NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `staff_login_history`
--

INSERT INTO `staff_login_history` (`staffAccountID`, `loginDate`)
VALUES ('SA00001', '2024-05-28 23:48:36'),
       ('SA00001', '2024-05-28 23:52:13'),
       ('SA00001', '2024-05-28 23:52:32'),
       ('SA00001', '2024-05-29 00:22:01'),
       ('SA00001', '2024-05-29 00:34:17'),
       ('SA00001', '2024-05-29 01:09:21'),
       ('SA00001', '2024-05-29 01:59:22'),
       ('SA00001', '2024-05-29 23:20:19'),
       ('SA00001', '2024-05-29 23:29:34'),
       ('SA00001', '2024-05-30 01:41:37'),
       ('SA00001', '2024-05-30 01:45:42'),
       ('SA00001', '2024-05-30 01:50:35'),
       ('SA00001', '2024-05-30 02:14:46'),
       ('SA00001', '2024-05-30 02:18:13'),
       ('SA00001', '2024-05-30 02:23:22'),
       ('SA00001', '2024-05-30 02:23:58'),
       ('SA00001', '2024-05-30 02:26:30'),
       ('SA00001', '2024-05-30 02:28:07'),
       ('SA00001', '2024-05-30 02:48:49'),
       ('SA00001', '2024-05-30 02:50:04'),
       ('SA00001', '2024-05-30 02:54:21'),
       ('SA00001', '2024-05-30 02:55:41'),
       ('SA00001', '2024-05-30 02:57:36'),
       ('SA00001', '2024-05-30 02:59:10'),
       ('SA00001', '2024-05-30 03:02:51'),
       ('SA00001', '2024-05-30 03:03:30'),
       ('SA00001', '2024-05-30 03:04:15'),
       ('SA00001', '2024-05-30 03:12:02'),
       ('SA00001', '2024-05-30 03:14:27'),
       ('SA00001', '2024-05-30 03:14:38'),
       ('SA00001', '2024-05-30 03:18:14'),
       ('SA00001', '2024-05-30 03:18:50'),
       ('SA00001', '2024-05-30 03:19:22'),
       ('SA00001', '2024-05-30 03:42:55'),
       ('SA00001', '2024-05-30 03:44:59'),
       ('SA00001', '2024-05-30 03:45:29'),
       ('SA00001', '2024-05-30 03:47:09'),
       ('SA00001', '2024-05-30 03:48:52'),
       ('SA00001', '2024-05-30 03:49:34'),
       ('SA00001', '2024-05-30 11:15:20'),
       ('SA00001', '2024-05-30 13:20:05'),
       ('SA00001', '2024-05-30 13:20:41'),
       ('SA00001', '2024-05-30 13:23:00'),
       ('SA00001', '2024-05-30 13:24:08'),
       ('SA00001', '2024-06-02 23:08:05'),
       ('SA00001', '2024-06-02 23:09:29'),
       ('SA00001', '2024-06-02 23:12:39'),
       ('SA00001', '2024-06-02 23:13:39'),
       ('SA00001', '2024-06-02 23:14:02'),
       ('SA00001', '2024-06-02 23:15:10'),
       ('SA00001', '2024-06-02 23:19:17'),
       ('SA00001', '2024-06-02 23:23:32'),
       ('SA00001', '2024-06-02 23:24:20'),
       ('SA00001', '2024-06-02 23:27:20'),
       ('SA00001', '2024-06-02 23:28:26'),
       ('SA00001', '2024-06-03 00:14:01'),
       ('SA00001', '2024-06-03 00:16:58'),
       ('SA00001', '2024-06-03 00:19:30'),
       ('SA00001', '2024-06-03 00:22:19'),
       ('SA00001', '2024-06-03 00:40:21'),
       ('SA00001', '2024-06-03 00:40:58'),
       ('SA00001', '2024-06-03 00:41:20'),
       ('SA00001', '2024-06-03 00:42:13'),
       ('SA00001', '2024-06-03 00:42:43'),
       ('SA00001', '2024-06-03 00:43:19'),
       ('SA00001', '2024-06-03 00:44:16'),
       ('SA00001', '2024-06-03 00:48:07'),
       ('SA00001', '2024-06-03 00:49:59'),
       ('SA00001', '2024-06-03 00:50:33'),
       ('SA00001', '2024-06-03 00:51:29'),
       ('SA00001', '2024-06-03 00:51:48'),
       ('SA00001', '2024-06-03 00:53:23'),
       ('SA00001', '2024-06-03 00:55:33'),
       ('SA00001', '2024-06-03 00:56:17'),
       ('SA00001', '2024-06-03 00:57:14'),
       ('SA00001', '2024-06-03 01:01:37'),
       ('SA00001', '2024-06-03 18:20:57'),
       ('SA00001', '2024-06-03 18:22:16'),
       ('SA00001', '2024-06-03 19:25:27'),
       ('SA00001', '2024-06-03 19:27:18'),
       ('SA00001', '2024-06-03 19:30:30'),
       ('SA00001', '2024-06-03 19:32:10'),
       ('SA00001', '2024-06-03 19:37:00'),
       ('SA00001', '2024-06-03 19:42:38'),
       ('SA00001', '2024-06-03 20:52:31'),
       ('SA00001', '2024-06-03 20:52:39'),
       ('SA00001', '2024-06-03 20:52:49'),
       ('SA00001', '2024-06-03 20:53:42'),
       ('SA00001', '2024-06-03 20:56:02'),
       ('SA00001', '2024-06-03 20:56:57'),
       ('SA00001', '2024-06-03 21:23:53'),
       ('SA00001', '2024-06-03 21:24:48'),
       ('SA00001', '2024-06-03 21:28:03'),
       ('SA00001', '2024-06-03 21:35:18'),
       ('SA00001', '2024-06-04 00:15:25'),
       ('SA00001', '2024-06-04 00:18:34'),
       ('SA00001', '2024-06-04 00:26:11'),
       ('SA00001', '2024-06-04 00:27:33'),
       ('SA00001', '2024-06-04 13:54:36'),
       ('SA00001', '2024-06-16 01:19:35'),
       ('SA00001', '2024-06-16 01:19:50'),
       ('SA00001', '2024-06-16 01:21:43'),
       ('SA00001', '2024-06-16 01:30:19'),
       ('SA00001', '2024-06-16 01:30:52'),
       ('SA00001', '2024-06-16 01:31:34'),
       ('SA00001', '2024-06-16 01:43:51'),
       ('SA00001', '2024-06-16 01:44:33'),
       ('SA00001', '2024-06-16 01:51:10'),
       ('SA00001', '2024-06-16 01:52:22'),
       ('SA00001', '2024-06-16 01:52:57'),
       ('SA00001', '2024-06-16 01:54:43'),
       ('SA00001', '2024-06-16 02:30:21'),
       ('SA00001', '2024-06-16 02:41:09'),
       ('SA00001', '2024-06-16 02:42:47'),
       ('SA00001', '2024-06-16 02:45:22'),
       ('SA00001', '2024-06-16 02:47:08'),
       ('SA00001', '2024-06-16 02:51:54'),
       ('SA00001', '2024-06-16 02:52:21'),
       ('SA00001', '2024-06-17 00:06:08'),
       ('SA00001', '2024-06-17 00:16:22'),
       ('SA00001', '2024-06-17 00:17:27'),
       ('SA00001', '2024-06-17 00:19:21'),
       ('SA00001', '2024-06-17 00:21:13'),
       ('SA00001', '2024-06-17 00:22:38'),
       ('SA00001', '2024-06-17 00:23:49'),
       ('SA00001', '2024-06-17 00:24:52'),
       ('SA00001', '2024-06-17 00:25:49'),
       ('SA00001', '2024-06-17 00:29:39'),
       ('SA00001', '2024-06-17 00:34:53'),
       ('SA00001', '2024-06-17 00:37:33'),
       ('SA00001', '2024-06-17 00:39:36'),
       ('SA00001', '2024-06-17 00:40:03'),
       ('SA00001', '2024-06-17 00:43:16'),
       ('SA00001', '2024-06-20 00:23:23'),
       ('SA00001', '2024-06-20 00:24:07'),
       ('SA00001', '2024-06-20 00:25:29'),
       ('SA00001', '2024-06-20 00:30:19'),
       ('SA00001', '2024-06-20 00:31:33'),
       ('SA00001', '2024-06-20 00:32:55'),
       ('SA00001', '2024-06-20 00:33:41'),
       ('SA00001', '2024-06-21 00:06:48'),
       ('SA00001', '2024-06-22 18:35:21'),
       ('SA00002', '2024-05-28 23:51:33'),
       ('SA00002', '2024-05-29 00:27:56'),
       ('SA00002', '2024-05-29 00:31:28'),
       ('SA00002', '2024-05-29 00:32:24'),
       ('SA00002', '2024-05-29 00:33:53'),
       ('SA00002', '2024-05-29 01:02:10'),
       ('SA00002', '2024-05-29 02:00:13'),
       ('SA00002', '2024-05-30 01:50:32'),
       ('SA00002', '2024-05-30 02:50:09'),
       ('SA00002', '2024-05-30 02:59:30'),
       ('SA00002', '2024-05-30 11:14:59'),
       ('SA00002', '2024-06-03 00:24:34'),
       ('SA00002', '2024-06-03 20:52:45'),
       ('SA00002', '2024-06-04 13:46:06'),
       ('SA00002', '2024-06-04 13:56:49'),
       ('SA00003', '2024-05-28 23:24:23'),
       ('SA00003', '2024-05-28 23:24:28'),
       ('SA00003', '2024-05-28 23:24:34'),
       ('SA00003', '2024-05-28 23:34:20'),
       ('SA00003', '2024-05-28 23:40:15'),
       ('SA00003', '2024-05-28 23:40:24'),
       ('SA00003', '2024-05-28 23:43:35'),
       ('SA00003', '2024-05-28 23:44:12'),
       ('SA00003', '2024-05-28 23:46:51'),
       ('SA00003', '2024-05-28 23:46:56'),
       ('SA00003', '2024-05-28 23:46:59'),
       ('SA00003', '2024-05-28 23:53:46'),
       ('SA00003', '2024-05-29 00:15:31'),
       ('SA00003', '2024-05-29 00:23:07'),
       ('SA00003', '2024-05-29 00:25:42'),
       ('SA00003', '2024-05-29 00:27:34'),
       ('SA00003', '2024-05-29 01:04:30'),
       ('SA00003', '2024-05-29 01:08:52'),
       ('SA00003', '2024-05-29 01:09:32'),
       ('SA00003', '2024-05-29 01:10:23'),
       ('SA00003', '2024-05-29 01:12:43'),
       ('SA00003', '2024-05-29 01:48:17'),
       ('SA00003', '2024-05-30 03:01:37'),
       ('SA00003', '2024-05-30 03:19:54'),
       ('SA00003', '2024-05-30 03:48:07'),
       ('SA00003', '2024-05-30 03:49:14'),
       ('SA00003', '2024-06-04 13:50:18'),
       ('SA00004', '2024-05-29 01:07:13'),
       ('SA00004', '2024-05-30 01:50:38'),
       ('SA00004', '2024-05-30 13:20:52'),
       ('SA00004', '2024-06-03 21:36:10'),
       ('SA00004', '2024-06-03 21:36:12'),
       ('SA00004', '2024-06-04 13:56:58'),
       ('SA00004', '2024-06-22 18:36:50'),
       ('SA00005', '2024-05-30 03:02:07'),
       ('SA00005', '2024-06-04 13:58:21'),
       ('SA00005', '2024-06-04 13:59:03'),
       ('SA00005', '2024-06-20 21:45:11'),
       ('SA00005', '2024-06-21 01:34:41'),
       ('SA00005', '2024-06-21 02:16:44'),
       ('SA00005', '2024-06-21 22:36:54'),
       ('SA00005', '2024-06-21 22:37:35'),
       ('SA00006', '2024-06-16 00:53:42'),
       ('SA00006', '2024-06-16 00:57:41'),
       ('SA00006', '2024-06-16 01:01:25'),
       ('SA00006', '2024-06-16 01:03:21'),
       ('SA00006', '2024-06-16 01:04:42'),
       ('SA00006', '2024-06-16 01:05:58'),
       ('SA00006', '2024-06-16 01:06:30'),
       ('SA00006', '2024-06-16 01:09:58'),
       ('SA00006', '2024-06-16 01:19:42'),
       ('SA00006', '2024-06-16 01:22:01'),
       ('SA00006', '2024-06-16 01:31:48'),
       ('SA00006', '2024-06-16 02:43:18'),
       ('SA00006', '2024-06-16 02:43:59'),
       ('SA00006', '2024-06-16 02:44:24'),
       ('SA00006', '2024-06-17 00:43:44'),
       ('SA00006', '2024-06-17 00:45:42'),
       ('SA00006', '2024-06-17 00:55:07'),
       ('SA00006', '2024-06-17 00:57:05'),
       ('SA00006', '2024-06-17 01:00:32'),
       ('SA00007', '2024-06-17 00:56:35'),
       ('SA00007', '2024-06-17 00:56:55'),
       ('SA00007', '2024-06-17 01:00:18');

-- --------------------------------------------------------

--
-- Table structure for table `supplier`
--

CREATE TABLE `supplier`
(
    `supplierID` char(10)    NOT NULL,
    `name`       varchar(50) NOT NULL,
    `phone`      char(20)    NOT NULL,
    `address`    varchar(70) NOT NULL,
    `country`    varchar(30) NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_general_ci;

--
-- Dumping data for table `supplier`
--

INSERT INTO `supplier` (`supplierID`, `name`, `phone`, `address`, `country`)
VALUES ('SIDBR00001', 'Metalúrgica RIOSULENSE S.A.', '+55-4735211200',
        'Rua Campos Salles, 1000 - Centro, Rio do Sul - SC, 89160-000', 'Brazil'),
       ('SIDCN00001', 'Guangzhou Jincheng Auto Group', '+86-208765 4321', 'No. 456, Guangzhou Avenue, Guangzhou',
        'China'),
       ('SIDCN00002', 'BYD Auto Co., Ltd.', '+86-75589888888', 'BYD Road, Pingshan District, Shenzhen, Guangdong',
        'China'),
       ('SIDIN00001', 'Tata Auto Components', '0987654321', '456 Connaught Place, New Delhi', 'India'),
       ('SIDIT00001', 'Fiat Chrysler Automobiles', '+39-01100', 'Corso Giovanni Agnelli, 200, Turin', 'Italy'),
       ('SIDJP00001', 'Denso Corporation', '+81-566255511', '1-1 Showa-cho, Kariya, Aichi 448-8661', 'Japan'),
       ('SIDKR00001', 'Hyundai Auto Parts', '0237456789', '123 Gangnam-gu, Seoul', 'South Korea'),
       ('SIDUK00001', 'Halfords Group PLC', '+44-03332001003', 'Icknield Street Drive, Washford, Redditch B98 0DE',
        'United Kingdom'),
       ('SIDUK00002', 'Euro Car Parts', '+44-02082313000', '1 Racecourse Industrial Estate, Uxbridge UB8 2LJ',
        'United Kingdom'),
       ('SIDUK00003', 'Febi Bilstein UK Ltd.', '+44-01227374724',
        'Gateway Business Park, Vauxhall Industrial Estate, Ruislip HA4 6QD', 'United Kingdom'),
       ('SIDUS00001', 'AutoZone, Inc.', '+1-9014956500', '123 South Front Street, Memphis, TN 38103', 'United States'),
       ('SIDUS00002', 'Reilly Auto Parts', '+14178623333', '233 S Patterson Ave, Springfield, MO 65802',
        'United States');

-- --------------------------------------------------------

--
-- Structure for view `shippedordertotals`
--
DROP TABLE IF EXISTS `shippedordertotals`;

CREATE ALGORITHM = UNDEFINED DEFINER =`root`@`localhost` SQL SECURITY DEFINER VIEW `shippedordertotals` AS
SELECT `o`.`orderID`                                                       AS `orderID`,
       `ca`.`customerAccountID`                                            AS `CustomerAccountID`,
       `cu`.`customerID`                                                   AS `CustomerID`,
       concat(`cu`.`firstName`, ' ', `cu`.`lastName`)                      AS `CustomerName`,
       `o`.`orderDate`                                                     AS `orderDate`,
       `o`.`status`                                                        AS `OrderStatus`,
       round(case
                 when `d`.`percentage` is not null then sum(`ol`.`quantity` * `ol`.`orderUnitPrice`) *
                                                        (1 - `d`.`percentage` / 100)
                 else sum(`ol`.`quantity` * `ol`.`orderUnitPrice`) end, 2) AS `orderTotal`
FROM (((((`order_` `o` join `customer_account` `ca`
          on (`o`.`customerAccountID` = `ca`.`customerAccountID`)) join `customer` `cu`
         on (`ca`.`customerID` = `cu`.`customerID`)) join `order_line` `ol`
        on (`o`.`orderID` = `ol`.`orderID`)) join `product` `p`
       on (`ol`.`partNumber` = `p`.`partNumber`)) left join `discount` `d` on (`o`.`discountID` = `d`.`discountID`))
WHERE `o`.`status` = 'Shipped'
GROUP BY `o`.`orderID`, `ca`.`customerAccountID`, `cu`.`customerID`, `cu`.`firstName`, `cu`.`lastName`, `o`.`orderDate`,
         `o`.`status`;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cart`
--
ALTER TABLE `cart`
    ADD PRIMARY KEY (`cartID`),
    ADD KEY `cart_fk` (`customerAccountID`);

--
-- Indexes for table `category`
--
ALTER TABLE `category`
    ADD PRIMARY KEY (`categoryID`);

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
    ADD PRIMARY KEY (`customerID`);

--
-- Indexes for table `customer_account`
--
ALTER TABLE `customer_account`
    ADD PRIMARY KEY (`customerAccountID`),
    ADD KEY `customerAccount_fk` (`customerID`);

--
-- Indexes for table `customer_dfadd`
--
ALTER TABLE `customer_dfadd`
    ADD PRIMARY KEY (`customerID`);

--
-- Indexes for table `customer_login_history`
--
ALTER TABLE `customer_login_history`
    ADD PRIMARY KEY (`customerAccountID`, `loginDate`);

--
-- Indexes for table `deliverman`
--
ALTER TABLE `deliverman`
    ADD PRIMARY KEY (`delivermanID`),
    ADD KEY `deliverman_fk` (`staffID`);

--
-- Indexes for table `deliveryrelay`
--
ALTER TABLE `deliveryrelay`
    ADD PRIMARY KEY (`RelayID`);

--
-- Indexes for table `department`
--
ALTER TABLE `department`
    ADD PRIMARY KEY (`deptID`);

--
-- Indexes for table `discount`
--
ALTER TABLE `discount`
    ADD PRIMARY KEY (`discountID`);

--
-- Indexes for table `favourite`
--
ALTER TABLE `favourite`
    ADD PRIMARY KEY (`customerID`, `itemID`),
    ADD KEY `itemID_fk` (`itemID`);

--
-- Indexes for table `feedback`
--
ALTER TABLE `feedback`
    ADD PRIMARY KEY (`feedbackID`),
    ADD KEY `feedback_fk` (`customerID`),
    ADD KEY `feedback_orderID_fk` (`orderID`),
    ADD KEY `feedback_staffID_fk` (`staffID`);

--
-- Indexes for table `instruction`
--
ALTER TABLE `instruction`
    ADD PRIMARY KEY (`orderID`);

--
-- Indexes for table `invoice`
--
ALTER TABLE `invoice`
    ADD PRIMARY KEY (`customerAccountID`, `orderID`),
    ADD KEY `invoice_fk2` (`orderID`);

--
-- Indexes for table `location`
--
ALTER TABLE `location`
    ADD PRIMARY KEY (`province`, `city`);

--
-- Indexes for table `order_`
--
ALTER TABLE `order_`
    ADD PRIMARY KEY (`orderID`),
    ADD KEY `order_fk2` (`discountID`),
    ADD KEY `order_fk3` (`feedbackID`),
    ADD KEY `order_fk4` (`customerAccountID`),
    ADD KEY `order_fk5` (`staffAccountID`);

--
-- Indexes for table `order_line`
--
ALTER TABLE `order_line`
    ADD PRIMARY KEY (`partNumber`, `orderID`),
    ADD KEY `order_line_fk2` (`orderID`);

--
-- Indexes for table `permission`
--
ALTER TABLE `permission`
    ADD PRIMARY KEY (`permissionID`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
    ADD PRIMARY KEY (`itemID`),
    ADD KEY `product_fk` (`partNumber`);

--
-- Indexes for table `product_in_cart`
--
ALTER TABLE `product_in_cart`
    ADD PRIMARY KEY (`cartID`, `itemID`),
    ADD KEY `product_in_cart_fk2` (`itemID`);

--
-- Indexes for table `resource`
--
ALTER TABLE `resource`
    ADD PRIMARY KEY (`id`);

--
-- Indexes for table `shipping_detail`
--
ALTER TABLE `shipping_detail`
    ADD PRIMARY KEY (`orderID`),
    ADD KEY `shipping_detail_fk2` (`delivermanID`),
    ADD KEY `shipping_detail_fk3` (`orderID`);

--
-- Indexes for table `spare_part`
--
ALTER TABLE `spare_part`
    ADD PRIMARY KEY (`partNumber`),
    ADD KEY `spare_part_fk1` (`supplierID`),
    ADD KEY `spare_part_fk2` (`categoryID`);

--
-- Indexes for table `staff`
--
ALTER TABLE `staff`
    ADD PRIMARY KEY (`staffID`),
    ADD KEY `staff_fk` (`deptID`),
    ADD KEY `staff_delivermanID_fk` (`delivermanID`);

--
-- Indexes for table `staff_account`
--
ALTER TABLE `staff_account`
    ADD PRIMARY KEY (`staffAccountID`) USING BTREE,
    ADD KEY `staffaccount_staffID_fk` (`staffID`);

--
-- Indexes for table `staff_account_permission`
--
ALTER TABLE `staff_account_permission`
    ADD PRIMARY KEY (`staffAccountID`, `permissionID`),
    ADD KEY `staff_account_permission_fk1` (`permissionID`);

--
-- Indexes for table `staff_login_history`
--
ALTER TABLE `staff_login_history`
    ADD PRIMARY KEY (`staffAccountID`, `loginDate`);

--
-- Indexes for table `supplier`
--
ALTER TABLE `supplier`
    ADD PRIMARY KEY (`supplierID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `resource`
--
ALTER TABLE `resource`
    MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cart`
--
ALTER TABLE `cart`
    ADD CONSTRAINT `cart_fk` FOREIGN KEY (`customerAccountID`) REFERENCES `customer_account` (`customerAccountID`);

--
-- Constraints for table `customer_account`
--
ALTER TABLE `customer_account`
    ADD CONSTRAINT `customerAccount_fk` FOREIGN KEY (`customerID`) REFERENCES `customer` (`customerID`);

--
-- Constraints for table `customer_dfadd`
--
ALTER TABLE `customer_dfadd`
    ADD CONSTRAINT `customerID_fk` FOREIGN KEY (`customerID`) REFERENCES `customer` (`customerID`);

--
-- Constraints for table `customer_login_history`
--
ALTER TABLE `customer_login_history`
    ADD CONSTRAINT `customer_login_history_fk` FOREIGN KEY (`customerAccountID`) REFERENCES `customer_account` (`customerAccountID`);

--
-- Constraints for table `deliverman`
--
ALTER TABLE `deliverman`
    ADD CONSTRAINT `deliverman_fk` FOREIGN KEY (`staffID`) REFERENCES `staff` (`staffID`);

--
-- Constraints for table `favourite`
--
ALTER TABLE `favourite`
    ADD CONSTRAINT `fk1` FOREIGN KEY (`customerID`) REFERENCES `customer` (`customerID`),
    ADD CONSTRAINT `item_fk` FOREIGN KEY (`itemID`) REFERENCES `product` (`itemID`);

--
-- Constraints for table `feedback`
--
ALTER TABLE `feedback`
    ADD CONSTRAINT `feedback_fk` FOREIGN KEY (`customerID`) REFERENCES `customer` (`customerID`),
    ADD CONSTRAINT `feedback_orderID_fk` FOREIGN KEY (`orderID`) REFERENCES `order_` (`orderID`),
    ADD CONSTRAINT `feedback_staffID_fk` FOREIGN KEY (`staffID`) REFERENCES `staff` (`staffID`);

--
-- Constraints for table `instruction`
--
ALTER TABLE `instruction`
    ADD CONSTRAINT `instrucion_fk` FOREIGN KEY (`orderID`) REFERENCES `order_` (`orderID`);

--
-- Constraints for table `invoice`
--
ALTER TABLE `invoice`
    ADD CONSTRAINT `invoice_fk1` FOREIGN KEY (`customerAccountID`) REFERENCES `customer_account` (`customerAccountID`),
    ADD CONSTRAINT `invoice_fk2` FOREIGN KEY (`orderID`) REFERENCES `order_` (`orderID`);

--
-- Constraints for table `order_`
--
ALTER TABLE `order_`
    ADD CONSTRAINT `order_fk2` FOREIGN KEY (`discountID`) REFERENCES `discount` (`discountID`),
    ADD CONSTRAINT `order_fk3` FOREIGN KEY (`feedbackID`) REFERENCES `feedback` (`feedbackID`),
    ADD CONSTRAINT `order_fk4` FOREIGN KEY (`customerAccountID`) REFERENCES `customer_account` (`customerAccountID`),
    ADD CONSTRAINT `order_fk5` FOREIGN KEY (`staffAccountID`) REFERENCES `staff_account` (`staffAccountID`);

--
-- Constraints for table `order_line`
--
ALTER TABLE `order_line`
    ADD CONSTRAINT `order_line_fk1` FOREIGN KEY (`partNumber`) REFERENCES `spare_part` (`partNumber`),
    ADD CONSTRAINT `order_line_fk2` FOREIGN KEY (`orderID`) REFERENCES `order_` (`orderID`);

--
-- Constraints for table `product`
--
ALTER TABLE `product`
    ADD CONSTRAINT `product_fk` FOREIGN KEY (`partNumber`) REFERENCES `spare_part` (`partNumber`);

--
-- Constraints for table `product_in_cart`
--
ALTER TABLE `product_in_cart`
    ADD CONSTRAINT `product_in_cart_fk1` FOREIGN KEY (`cartID`) REFERENCES `cart` (`cartID`),
    ADD CONSTRAINT `product_in_cart_fk2` FOREIGN KEY (`itemID`) REFERENCES `product` (`itemID`);

--
-- Constraints for table `shipping_detail`
--
ALTER TABLE `shipping_detail`
    ADD CONSTRAINT `shipping_detail_fk2` FOREIGN KEY (`delivermanID`) REFERENCES `deliverman` (`delivermanID`),
    ADD CONSTRAINT `shipping_detail_fk3` FOREIGN KEY (`orderID`) REFERENCES `order_` (`orderID`);

--
-- Constraints for table `spare_part`
--
ALTER TABLE `spare_part`
    ADD CONSTRAINT `spare_part_fk1` FOREIGN KEY (`supplierID`) REFERENCES `supplier` (`supplierID`),
    ADD CONSTRAINT `spare_part_fk2` FOREIGN KEY (`categoryID`) REFERENCES `category` (`categoryID`);

--
-- Constraints for table `staff`
--
ALTER TABLE `staff`
    ADD CONSTRAINT `staff_delivermanID_fk` FOREIGN KEY (`delivermanID`) REFERENCES `deliverman` (`delivermanID`),
    ADD CONSTRAINT `staff_fk` FOREIGN KEY (`deptID`) REFERENCES `department` (`deptID`);

--
-- Constraints for table `staff_account`
--
ALTER TABLE `staff_account`
    ADD CONSTRAINT `staffaccount_staffID_fk` FOREIGN KEY (`staffID`) REFERENCES `staff` (`staffID`);

--
-- Constraints for table `staff_account_permission`
--
ALTER TABLE `staff_account_permission`
    ADD CONSTRAINT `fk2` FOREIGN KEY (`staffAccountID`) REFERENCES `staff_account` (`staffAccountID`),
    ADD CONSTRAINT `staff_account_permission_fk1` FOREIGN KEY (`permissionID`) REFERENCES `permission` (`permissionID`),
    ADD CONSTRAINT `staff_account_permission_fk2` FOREIGN KEY (`staffAccountID`) REFERENCES `staff_account` (`staffAccountID`);

--
-- Constraints for table `staff_login_history`
--
ALTER TABLE `staff_login_history`
    ADD CONSTRAINT `staff_login_history_fk` FOREIGN KEY (`staffAccountID`) REFERENCES `staff_account` (`staffAccountID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT = @OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS = @OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION = @OLD_COLLATION_CONNECTION */;
