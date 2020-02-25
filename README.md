# Shopping Basket
ShoppingBasket is a class library that allows a customer to add
and remove products and provides a total cost of the basket including
all applicable discounts.

The library is written in .NET 4.7.2. Besides the class library
project (ShoppingBasket.Service) the solution contains a unit 
tests project (ShoppingBasket.Tests). Tests were written with
NUnit.

A user can add one of the following items in his basket

| Id | Name   | Price |
|:-: |:------:| :----:|
| 1  | Butter | $0.8  |
| 2  | Milk   | $1.15 |
| 3  | Bread  | $1.00 |

Depending on the contents of the basket some of the
following discounts may apply:
- Buy 2 butters and get one bread at 50% off
- Buy 3 milks and get the 4th milk for free

### Assumptions
- Multiple discounts can be applied to a basket.
- Adding of new types of items and discounts is handled
by another component.
- The user id passed in the methods is that of a valid user.
User authentication is handled by the component consuming the 
ShoppingBasket class.