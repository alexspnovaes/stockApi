# Stock API

This API was created to connect to the financial chat and get the stock cotes and return to the chat app using the rabbit mq queue message.

## Installation

It´s a very simple API using .NET 7 and rabbit MQ, you can install manually or use the docker compose in the financial chat to install the rabbit MQ. There is no database.

.NET 7: https://dotnet.microsoft.com/en-us/download/dotnet/7.0

RabbitMQ: https://www.rabbitmq.com/


## Usage

Only needs to point to the URL in the config of the financial chat. There is a small swagger documentation to help. There is only one post endpoint who receives the room id, user id and the cote code. The API connects to the quote endpoint, this endpoint returns a csv, and the stock api read this file and process. After this, if everything is ok, send a message to the queue. 

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[CopyRight 2022]
