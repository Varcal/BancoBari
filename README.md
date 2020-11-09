# BancoBari
Um exemplo simples de comunicação entre Microserviços


Para executar os serviços primeiro é preciso ter o Docker instalado.
No diretorio server existe um arquivo docker-compose com as depedências necessárias para executar os projetos
Abra um powershell ou terminar no diretorio server e execute docker-compose up -d

Após a execução do docker-compose já podemos rodar as duas APIs HelloWorldAPI e CustomerAPI.

HelloWorldAPI é producer de mensagens de HelloWorld e consumer da CustomerAPI;
Para iniciar o envio das mensagens a cada 5 segundos, basta executar o endpoint disponibilizado pelo OpenAPI e será inciado o envio das mensagens, no console é possível ver os logs de envio e recebimento das mensagens.

CustomerAPI é producer de mensagens de HelloWorld e consumer da HelloWorldAPI;
Para enviar uma mensagens de Customer, basta executar o endpoint disponibilizado pelo OpenAPI e será enviada uma mensagem, no console é possível ver os logs de envio e recebimento das mensagens.

