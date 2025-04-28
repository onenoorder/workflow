# Specification


### 1. Data Types
- **Strings**: `"text"`
- **Numbers**: `123`, `12` `3`
- **Booleans**: `true  false`

#### Result only
- **User**: `User { Name = Alice, Surname = Johnson, Age = 28 }`

### 2. Variables
- Assign values using `=`

`name = "Alice"`

### 3. Operators
| Operator     | Description              | Example                    |
|--------------|--------------------------|----------------------------|
| `~`          | String concatenation     | `"Hi, " ~ "Alice"`         |
| `+ - * /`    | Numeric operations       | `total = price * quantity` |
| `> < equals` | Comparison operations    | `price equals 25`          |
| `.`          | Member access operations | `user.Name`                |


### 4. Functions
#### Print
```
print("Hello world")
```
#### Users
| Name       | Description                    | Example                                           |
|------------|--------------------------------|---------------------------------------------------|
| Find       | Finds a user base on full name | `Users.Find("Gerard Kroes") `                     |
| All        | Returns a list of all users    | `Users.All()`                                     |
| GetInitial | Get the initial of a user      | `Users.GetInitial(user)`                          |
| Add        | Add a user                     | `Users.Add("Alice", "Bobbinson", 34) `  |

### 5. Control Flow
#### If/Else
```
if age >= 18 
then
  print("Adult")
else
  print("Minor")
endif


if age >= 18 
then
  print("Adult")
endif

```
#### Loop
```
loop 2
    print("wow")
endloop

loop 2 num
    print(num)
endloop

loop Users.All() currentUser
    print(currentUser)
endloop
```