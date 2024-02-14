-- Drop tables if they exist
DROP TABLE IF EXISTS AddIn_OrderDetail;
DROP TABLE IF EXISTS AddIn;
DROP TABLE IF EXISTS OrderDetail;
DROP TABLE IF EXISTS Item;
DROP TABLE IF EXISTS ItemSize;
DROP TABLE IF EXISTS ItemType;
DROP TABLE IF EXISTS Ice;
DROP TABLE IF EXISTS Sweetness;
DROP TABLE IF EXISTS [Transaction];
DROP TABLE IF EXISTS [OrderStatus];
DROP TABLE IF EXISTS Rating;
DROP TABLE IF EXISTS [Store];
DROP TABLE IF EXISTS Contact;

-- Create ItemType table
CREATE TABLE ItemType (
    itemTypeID INTEGER PRIMARY KEY IDENTITY(1,1),
    itemTypeName VARCHAR(50) NOT NULL
);

-- Create ItemSize table
CREATE TABLE ItemSize (
    sizeID INTEGER PRIMARY KEY IDENTITY(1,1),
    sizeName VARCHAR(30) NOT NULL,
    priceModifier DECIMAL(10, 2) NOT NULL
);

-- Create Sweetness table
CREATE TABLE Sweetness (
    sweetnessID INTEGER PRIMARY KEY IDENTITY(1,1),
    sweetnessPercent DECIMAL(5, 2) NOT NULL
);

-- Create Ice table
CREATE TABLE Ice (
    iceID INTEGER PRIMARY KEY IDENTITY(1,1),
    icePercent DECIMAL(5, 2) NOT NULL
);

-- Create AddIn table
CREATE TABLE AddIn (
    addInID INTEGER PRIMARY KEY IDENTITY(1,1),
    addInName VARCHAR(30) NOT NULL,
    priceModifier DECIMAL(10, 2) NOT NULL,
    urlString TEXT
);

-- Create Store table
CREATE TABLE [Store] (
    storeID INTEGER PRIMARY KEY IDENTITY(1,1),
    storeHours VARCHAR(255) NOT NULL
);

-- Create OrderStatus table
CREATE TABLE [OrderStatus] (
    statusID INTEGER PRIMARY KEY IDENTITY(1,1),
    isOrdered BIT NOT NULL,
    isPickedUp BIT NOT NULL
);

-- Create Contact table
CREATE TABLE Contact (
    userID INTEGER PRIMARY KEY IDENTITY(1,1),
    firstName VARCHAR(30) NOT NULL,
    lastName VARCHAR(30),
    phoneNumber VARCHAR(20) NOT NULL,
    email VARCHAR(50) NOT NULL,
    unit INTEGER,
    street VARCHAR(50),
    city VARCHAR(50),
    province VARCHAR(20),
    postalCode VARCHAR(10),
    birthDate DATE,
    isDrinkRedeemed BIT NOT NULL
);

-- Create Transaction table
CREATE TABLE [Transaction] (
    transactionID VARCHAR(30) PRIMARY KEY,
    dateOrdered DATE NOT NULL,
    isOrdered BIT NOT NULL,
    isPickedUp BIT NOT NULL,
    storeID INTEGER NOT NULL,
    userID INTEGER NOT NULL,
    statusID INTEGER, -- Assuming this is the foreign key to OrderStatus table
    FOREIGN KEY (storeID) REFERENCES Store(storeID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (userID) REFERENCES Contact(userID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (statusID) REFERENCES OrderStatus(statusID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- Create Rating table
CREATE TABLE Rating (
    ratingID INTEGER PRIMARY KEY IDENTITY(1,1),
    rating VARCHAR(5) NOT NULL,
    date DATE NOT NULL,
    comment VARCHAR(255) NOT NULL,
    storeID INTEGER NOT NULL,
    userID INTEGER NOT NULL,
    FOREIGN KEY (storeID) REFERENCES Store(storeID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (userID) REFERENCES Contact(userID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- Create Item table
CREATE TABLE Item (
    itemID INTEGER PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    basePrice DECIMAL(10, 2) NOT NULL,
    inventory INTEGER NOT NULL,
    urlString TEXT,
    itemTypeID INTEGER,
    FOREIGN KEY (itemTypeID) REFERENCES ItemType(itemTypeID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- Create OrderDetail table
CREATE TABLE OrderDetail (
    orderDetailID VARCHAR(30) PRIMARY KEY,
    price DECIMAL(10, 2) NOT NULL,
    quantity INTEGER NOT NULL,
    isBirthdayDrink BIT NOT NULL,
    promoValue DECIMAL(10, 2),
    itemID INTEGER NOT NULL,
    transactionID VARCHAR(30) NOT NULL,
    sizeID INTEGER NOT NULL,
    sweetnessID INTEGER,
    iceID INTEGER,
    FOREIGN KEY (itemID) REFERENCES Item(itemID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (transactionID) REFERENCES [Transaction](transactionID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (sizeID) REFERENCES ItemSize(sizeID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (sweetnessID) REFERENCES Sweetness(sweetnessID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (iceID) REFERENCES Ice(iceID) ON UPDATE CASCADE ON DELETE CASCADE,
);

-- Create AddIn_OrderDetail table
CREATE TABLE AddIn_OrderDetail (
    addInID INTEGER NOT NULL,
    orderDetailID VARCHAR(30) NOT NULL,
    quantity INTEGER NOT NULL,
    PRIMARY KEY (addInID, orderDetailID),
    FOREIGN KEY (addInID) REFERENCES AddIn(addInID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (orderDetailID) REFERENCES OrderDetail(orderDetailID) ON UPDATE CASCADE ON DELETE CASCADE
);


-- Insert statements for Ice table
INSERT INTO Ice (icePercent) VALUES (100);
INSERT INTO Ice (icePercent) VALUES (75);
INSERT INTO Ice (icePercent) VALUES (50);
INSERT INTO Ice (icePercent) VALUES (25);
INSERT INTO Ice (icePercent) VALUES (0);

-- Insert statements for Sweetness table
INSERT INTO Sweetness (sweetnessPercent) VALUES (100);
INSERT INTO Sweetness (sweetnessPercent) VALUES (75);
INSERT INTO Sweetness (sweetnessPercent) VALUES (50);
INSERT INTO Sweetness (sweetnessPercent) VALUES (25);
INSERT INTO Sweetness (sweetnessPercent) VALUES (0);

-- Insert statements for ItemType table
INSERT INTO ItemType (itemTypeName) VALUES ('Milk Tea');
INSERT INTO ItemType (itemTypeName) VALUES ('Fruit Tea');
INSERT INTO ItemType (itemTypeName) VALUES ('Slush');

-- Milk Teas
INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Matcha Milk Tea', 'A delightful blend of matcha and creamy milk.', 2.99, 100, 1);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Taro Milk Tea', 'Experience the unique flavor of taro in a refreshing milk tea.', 2.99, 100, 1);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Brown Sugar Milk Tea', 'Indulge in the rich taste of brown sugar infused in milk tea.', 3.49, 100, 1);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Sips Milk Tea', 'Our classic and flavorful Sips Milk Tea.', 2.99, 100, 1);

-- Fruit Teas
INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('PassionFruit Tea', 'A tropical burst of passion fruit in a refreshing tea.', 3.29, 100, 2);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Peach Kiwi Tea', 'Experience the perfect harmony of peach and kiwi in tea.', 3.49, 100, 2);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Mango Tea', 'Enjoy the sweetness of mango in our delightful tea blend.', 3.29, 100, 2);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Wintermelon Tea', 'Refresh yourself with the cooling taste of wintermelon tea.', 3.29, 100, 2);

-- Slushes
INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Watermelon Raspberry Slush', 'Savor the unique and sweet taste of watermelon and raspberry in a slush.', 4.49, 100, 3);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Taro Slush', 'Experience the rich and creamy taro in a delightful slush.', 4.29, 100, 3);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Strawberry Slush', 'Enjoy the freshness of strawberry in a chilled slush drink.', 4.29, 100, 3);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID)
VALUES ('Coffee Slush', 'A delightful mix of coffee flavor in a frosty slush.', 4.49, 100, 3);
