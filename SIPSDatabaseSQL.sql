-- Drop tables if they exist
DROP TABLE IF EXISTS AddIn_OrderDetail;
DROP TABLE IF EXISTS AddIn;
DROP TABLE IF EXISTS OrderDetail;
DROP TABLE IF EXISTS Item;
DROP TABLE IF EXISTS ItemSize;
DROP TABLE IF EXISTS ItemType;
DROP TABLE IF EXISTS ImageStores;
DROP TABLE IF EXISTS Ice;
DROP TABLE IF EXISTS Sweetness;
DROP TABLE IF EXISTS [Transaction];
DROP TABLE IF EXISTS [OrderStatus];
DROP TABLE IF EXISTS Rating;
DROP TABLE IF EXISTS [Store];
DROP TABLE IF EXISTS Contact;
DROP TABLE IF EXISTS MilkChoice;

-- Create ItemType table
CREATE TABLE ItemType (
    itemTypeID INTEGER PRIMARY KEY IDENTITY(1,1),
    itemTypeName VARCHAR(50) NOT NULL
);

-- Create ImageStores table
CREATE TABLE ImageStores( 
    ImageID    INTEGER IDENTITY(1,1) NOT NULL,
    [FileName] VARCHAR(20)       NOT NULL,
    [Image]    VARBINARY(MAX)    NOT NULL, 
    CONSTRAINT PK_ImageStores PRIMARY KEY CLUSTERED ( ImageId ASC )
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
    sweetnessPercent VARCHAR(30) NOT NULL
);

-- Create Ice table
CREATE TABLE Ice (
    iceID INTEGER PRIMARY KEY IDENTITY(1,1),
    icePercent VARCHAR(30) NOT NULL
);

-- Create AddIn table
CREATE TABLE AddIn (
    addInID INTEGER PRIMARY KEY IDENTITY(1,1),
    addInName VARCHAR(30) NOT NULL,
    priceModifier DECIMAL(10, 2) NOT NULL,
);

-- Create Store table
CREATE TABLE [Store] (
    storeID INTEGER PRIMARY KEY IDENTITY(1,1),
    storeHours VARCHAR(255) NOT NULL
);

-- Create OrderStatus table
CREATE TABLE [OrderStatus] (
    statusID INTEGER PRIMARY KEY IDENTITY(1,1),
    isCompleted BIT NOT NULL
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
    storeID INTEGER NOT NULL,
    userID INTEGER NOT NULL,
    statusID INTEGER NOT NULL, -- Assuming this is the foreign key to OrderStatus table
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
    itemTypeID INTEGER,
    imageID INTEGER,
    hasMilk BIT NOT NULL
    FOREIGN KEY (itemTypeID) REFERENCES ItemType(itemTypeID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (imageID) REFERENCES ImageStores(ImageID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- Create MilkChoice table
CREATE TABLE MilkChoice (
    milkChoiceID INTEGER PRIMARY KEY IDENTITY(1,1),
    milkType VARCHAR(50) NOT NULL,
    priceModifier DECIMAL(10, 2) NOT NULL
);

-- Create OrderDetail table
CREATE TABLE OrderDetail (
    orderDetailID INT IDENTITY(1,1) PRIMARY KEY, -- Use INT for auto-incrementing ID
    price DECIMAL(10, 2) NOT NULL,
    quantity INTEGER NOT NULL,
    isBirthdayDrink BIT NOT NULL,
    promoValue DECIMAL(10, 2),
    itemID INTEGER NOT NULL,
    transactionID VARCHAR(30) NOT NULL,
    sizeID INTEGER NOT NULL,
    sweetnessID INTEGER,
    iceID INTEGER,
    milkChoiceID INTEGER, -- New column for MilkChoice
    FOREIGN KEY (itemID) REFERENCES Item(itemID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (transactionID) REFERENCES [Transaction](transactionID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (sizeID) REFERENCES ItemSize(sizeID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (sweetnessID) REFERENCES Sweetness(sweetnessID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (iceID) REFERENCES Ice(iceID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (milkChoiceID) REFERENCES MilkChoice(milkChoiceID) ON UPDATE CASCADE ON DELETE CASCADE -- New foreign key
);

-- Create AddIn_OrderDetail table
CREATE TABLE AddIn_OrderDetail (
    addInID INTEGER NOT NULL,
    orderDetailID INT NOT NULL,
    quantity INTEGER NOT NULL,
    PRIMARY KEY (addInID, orderDetailID),
    FOREIGN KEY (addInID) REFERENCES AddIn(addInID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (orderDetailID) REFERENCES OrderDetail(orderDetailID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- Pre-populate Sweetness table
INSERT INTO Sweetness (sweetnessPercent) VALUES ('0% Sweet');
INSERT INTO Sweetness (sweetnessPercent) VALUES ('25% Sweet');
INSERT INTO Sweetness (sweetnessPercent) VALUES ('50% Sweet');
INSERT INTO Sweetness (sweetnessPercent) VALUES ('75% Sweet');
INSERT INTO Sweetness (sweetnessPercent) VALUES ('100% Sweet');

-- Pre-populate Ice table
INSERT INTO Ice (icePercent) VALUES ('No Ice');
INSERT INTO Ice (icePercent) VALUES ('Less Ice');
INSERT INTO Ice (icePercent) VALUES ('Regular Ice');
INSERT INTO Ice (icePercent) VALUES ('Extra Ice');

-- Pre-populate MilkChoice table
INSERT INTO MilkChoice (milkType, priceModifier) VALUES ('Regular', 0.00);
INSERT INTO MilkChoice (milkType, priceModifier) VALUES ('Soy', 0.80);
INSERT INTO MilkChoice (milkType, priceModifier) VALUES ('Almond', 0.80);
INSERT INTO MilkChoice (milkType, priceModifier) VALUES ('Oat', 0.80);
INSERT INTO MilkChoice (milkType, priceModifier) VALUES ('No Milk', 0.00);

-- Pre-populate AddIn table
INSERT INTO AddIn (addInName, priceModifier) VALUES ('Pearls', 1.25);
INSERT INTO AddIn (addInName, priceModifier) VALUES ('Sago', 1.25);
INSERT INTO AddIn (addInName, priceModifier) VALUES ('Lychee Jelly', 1.25);
INSERT INTO AddIn (addInName, priceModifier) VALUES ('Pudding', 1.25);

-- Insert statements for ItemType table
INSERT INTO ItemType (itemTypeName) VALUES ('Milk Tea');
INSERT INTO ItemType (itemTypeName) VALUES ('Fruit Tea');
INSERT INTO ItemType (itemTypeName) VALUES ('Slush');

-- Milk Teas
INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Matcha Milk Tea', 'A delightful blend of matcha and creamy milk.', 4.99, 100, 1, 1);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Taro Milk Tea', 'Experience the unique flavor of taro in a refreshing milk tea.', 4.99, 100, 1, 1);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Brown Sugar Milk Tea', 'Indulge in the rich taste of brown sugar infused in milk tea.', 5.49, 100, 1, 1);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Sips Milk Tea', 'Our classic and flavorful Sips Milk Tea.', 3.99, 100, 1, 1);

-- Fruit Teas
INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('PassionFruit Tea', 'A tropical burst of passion fruit in a refreshing tea.', 5.69, 100, 2, 0);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Peach Kiwi Tea', 'Experience the perfect harmony of peach and kiwi in tea.', 5.99, 100, 2, 0);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Mango Tea', 'Enjoy the sweetness of mango in our delightful tea blend.', 5.99, 100, 2, 0);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Wintermelon Tea', 'Refresh yourself with the cooling taste of wintermelon tea.', 6.29, 100, 2, 0);

-- Slushes
INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Watermelon Raspberry Slush', 'Savor the unique and sweet taste of watermelon and raspberry in a slush.', 6.49, 100, 3, 0);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Taro Slush', 'Experience the rich and creamy taro in a delightful slush.', 6.29, 100, 3, 1);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Strawberry Slush', 'Enjoy the freshness of strawberry in a chilled slush drink.', 5.99, 100, 3, 0);

INSERT INTO Item (name, description, basePrice, inventory, itemTypeID, hasMilk)
VALUES ('Coffee Slush', 'A delightful mix of coffee flavor in a frosty slush.', 5.99, 100, 3, 1);


-- OrderStatus
INSERT INTO [OrderStatus] (isCompleted) VALUES (0);
INSERT INTO [OrderStatus] (isCompleted) VALUES (1);

INSERT INTO Contact (firstName, lastName, phoneNumber, email, unit, street, city, province, postalCode, birthDate, isDrinkRedeemed)
VALUES ('everest', 'shi', 'YOUR_PHONE_NUMBER', 'shi.everest@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO Contact (firstName, lastName, phoneNumber, email, unit, street, city, province, postalCode, birthDate, isDrinkRedeemed)
VALUES ('eunice', 'ssd', 'YOUR_PHONE_NUMBER', 'eunicezh10@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO Contact (firstName, lastName, phoneNumber, email, unit, street, city, province, postalCode, birthDate, isDrinkRedeemed)
VALUES ('sydnee', 'ssd', 'YOUR_PHONE_NUMBER', 's.snowball@hotmail.com', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO Contact (firstName, lastName, phoneNumber, email, unit, street, city, province, postalCode, birthDate, isDrinkRedeemed)
VALUES ('pam', 'ssd', 'YOUR_PHONE_NUMBER', 'pampragides@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO Contact (firstName, lastName, phoneNumber, email, unit, street, city, province, postalCode, birthDate, isDrinkRedeemed)
VALUES ('samaneh', 'ssd', 'YOUR_PHONE_NUMBER', 'sama.heshmatzadeh@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, 0);



@section Scripts {
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Function to reset form elements inside the modal
            function resetModal(modalId) {
                // Reset radio buttons for ice level
                var iceOptions = document.querySelectorAll('#' + modalId + ' input[name^="iceOptions_"]');
                iceOptions.forEach(function (input) {
                    input.checked = input.value === "3"; // Check if the value matches "Regular Ice"
                });
                // Reset radio buttons for sweetness level
                var sweetnessOptions = document.querySelectorAll('#' + modalId + ' input[name^="sweetnessOptions_"]');
                sweetnessOptions.forEach(function (input) {
                    input.checked = input.value === "5"; // Check if the value matches "100% Sweet"
                });
                // Reset radio buttons for milk choice
                var milkOptions = document.querySelectorAll('#' + modalId + ' input[name^="milkOptions_"]');
                milkOptions.forEach(function (input) {
                    input.checked = input.value === "1"; // Check if the value matches "Regular"
                });
                // Reset checkboxes for add-ins
                var addInOptions = document.querySelectorAll('#' + modalId + ' input[name^="addInOptions_"]');
                addInOptions.forEach(function (input) {
                    input.checked = input.defaultChecked;
                });

                // Reset Quantity
                var quantityInput = document.querySelector('#quantityInput_' + modalId.split('_')[1]);
                if (quantityInput) {
                    quantityInput.value = 1;
                }

                // Reset the base price
                var basePriceElement = document.querySelector('#' + modalId + ' span[id^="modalPrice_"]');
                if (basePriceElement) {
                    var initialPrice = basePriceElement.getAttribute('data-base-price');
                    basePriceElement.textContent = initialPrice;
                }
                
            }


            // Get references to the modal and its close buttons
            var closeModalButtons = document.querySelectorAll('[data-bs-dismiss="modal"]');
            // Add event listener for modal close buttons
            closeModalButtons.forEach(function (button) {
                button.addEventListener('click', function () {
                    // Find the closest modal parent of the button
                    var modal = button.closest('.modal');
                    if (modal) {
                        var modalId = modal.getAttribute('id');
                        resetModal(modalId);
                    }
                });
            });

            // Get references to the add to cart buttons
            var addToCartButtons = document.querySelectorAll('.add-to-cart');

            // Add event listener for add to cart button click
            addToCartButtons.forEach(function (button) {
                button.addEventListener('click', function () {
                    // Add your logic to add item to cart here
                });
            });
        });

        var totalQuantity = 0;
        var totalAmount = 0;

        function generateUniqueItemId(itemId, ice, sweetness, milkType, addInNames) {
            // Convert add-in names array to a string
            let addInNamesStr = addInNames.map(addIn => addIn.AddInName).join(",");
            // Create a unique identifier by combining the item ID with selected options
            let uniqueItemId = `${itemId}-${ice}-${sweetness}-${milkType}-${addInNamesStr}`;
            // Use a simple hash function or encoding if necessary to shorten or standardize the unique ID
            return uniqueItemId;
        }

        function AddToCart(itemId) {
            // Show the add to cart message
            $('.add-to-cart-message').fadeIn('slow');
            // Set a timeout to hide the message after 5 seconds
            setTimeout(function () {
                $('.add-to-cart-message').fadeOut('slow');
            }, 1500);
            // temporarily update the cart total until page is refreshed
            var checkoutTotal = document.getElementById('checkout-icon');
            var quantity = parseInt(inputField.value);
            // Check if quantity is a positive integer
            if (quantity <= 0 || isNaN(quantity) || !Number.isInteger(quantity)) {
                alert("Please enter a valid quantity.");
                return;

            var intTotal = parseInt(quantity) + 1;

            // Update the data-value attribute with the new total
            checkoutTotal.setAttribute('value', intTotal);

            // Collect item details
            // alert("function AddToCart(), itemId ==> " + $('#item-' + itemId + '-name').html() + ", price ==> " + $('#item-' + itemId + '-price').html());
            var iceLabel = document.querySelector('input[name="iceOptions_' + itemId + '"]:checked').nextElementSibling;
            var ice = iceLabel ? iceLabel.textContent.trim() : '';

            var sweetnessLabel = document.querySelector('input[name="sweetnessOptions_' + itemId + '"]:checked').nextElementSibling;
            var sweetness = sweetnessLabel ? sweetnessLabel.textContent.trim() : '';

            var addInOptions = document.querySelectorAll('input[name="addInOptions_' + itemId + '"]:checked');

            var milkTypeLabel = document.querySelector('input[name="milkOptions_' + itemId + '"]:checked').nextElementSibling;
            var milkType = milkTypeLabel ? milkTypeLabel.textContent.trim() : '';
            var milkPriceModifier = parseFloat(document.querySelector('input[name="milkOptions_' + itemId + '"]:checked').getAttribute('data-price-modifier'));

            var addInNames = [];
            addInOptions.forEach(function (option) {
                var addInLabel = option.nextElementSibling;
                if (addInLabel) {
                    var addInName = addInLabel.textContent.trim();
                    addInNames.push({
                        AddInName: addInName,
                        // You can add other properties of AddIn here if needed
                    });
                }
            });
            // alert('#modalPrice_' + itemId + ' ==> ' + $('#modalPrice_' + itemId).html());
            var uniqueItemId = generateUniqueItemId(
                itemId, 
                ice, 
                sweetness, 
                milkType, 
                addInNames // Make sure this is the array of objects {AddInName: "name"}
            );

            var item = {
                ItemId: itemId,
                UniqueItemId: uniqueItemId,
                BasePrice: parseFloat($('#item-' + itemId + '-price').html()),
                SubTotal: parseFloat($('#modalPrice_' + itemId).html()),
                Name: $('#item-' + itemId + '-name').html(),
                IcePercent: ice,
                SweetnessPercent: sweetness,
                MilkType: milkType,
                AddInNames: addInNames,
                Quantity: 1// Assign the populated list of AddInOrderDetail objects
            };

            // Get selected options from the modal
            var modalId = 'customizeModal_' + itemId;
            var iceOptions = document.querySelectorAll('#' + modalId + ' input[name="iceOptions"]:checked');
            var sweetnessOptions = document.querySelectorAll('#' + modalId + ' input[name="sweetnessOptions"]:checked');
            var milkOption = document.querySelector('#' + modalId + ' input[name="milkOptions"]:checked');
            var addInOptions = document.querySelectorAll('#' + modalId + ' input[name="addInOptions"]:checked');
            // Update item object with selected options
            if (iceOptions.length > 0) {
                item.ice = iceOptions[0].value;
            }
            if (sweetnessOptions.length > 0) {
                item.sweetness = sweetnessOptions[0].value;
            }
            if (milkOption) {
                item.milkType = milkOption.value;
                item.milkPriceModifier = parseFloat(milkOption.getAttribute('data-price-modifier'));
            }
            addInOptions.forEach(function (option) {
                item.addInNames.push(option.value);
                item.addInPriceModifiers.push(parseFloat(option.getAttribute('data-price-modifier')));
            });
            console.log(item);
            // Send item details to the server to add to the cart
            fetch('/Transaction/AddToCart', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(item)
            })
                .then(response => {
                    if (response.ok) {
                        console.log("added to cart");
                        // Optionally, update the UI to reflect that the item was added to the cart
                    } else {
                        // Handle errors if needed
                        console.log('FAIL');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    // Handle errors if needed
                });
        }

        function formatCurrency(amount) {
            return '$' + amount.toFixed(2) + ' ' + 'CAD';
        }

        // function to handle milk price modifiers
        function AdjustPrice(price, priceModifier, id, checkbox) {
            var basePrice = document.getElementById("modalPrice_" + id);
            var price = parseFloat(basePrice.textContent);
            var newPrice = price - currentPriceModifier;
            newTotal = newPrice + parseFloat(priceModifier);
            basePrice.textContent = (newTotal).toFixed(2);
            currentPriceModifier = priceModifier;
        }

        // Keep track of the current price modifier
        var currentPriceModifier = 0;

        //function to handle add on price modifiers
        function AddOnPrice(priceModifier, id, checkbox) {
            console.log(checkbox.checked)
            // if checkbox is checked, add the price modifier to the total
            if (checkbox.checked) {
                var basePrice = document.getElementById('modalPrice_' + id);
                console.log(basePrice.textContent);
                var price = parseFloat(basePrice.textContent);
                console.log(price == 4.99);
                var newTotal = price + parseFloat(priceModifier);
                basePrice.textContent = (newTotal).toFixed(2)
            }
            // if checkbox is unchecked, subtract the price modifier from the total
            else {
                var basePrice = document.getElementById('modalPrice_' + id);
                var price = parseFloat(basePrice.textContent);
                var newTotal = price - parseFloat(priceModifier);
                basePrice.textContent = (newTotal).toFixed(2);
            }
        }
    </script>
        }
}
