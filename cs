//TCP SERVER
#include <stdio.h>      
#include <string.h>     
#include <sys/socket.h>
#include <netinet/in.h> 
#include <unistd.h>    

#define PORT 8080 // Define a port number

int main() {
    int server_fd, client_fd;
    struct sockaddr_in server_addr, client_addr;
    char buffer[1024] = {0};
    const char *reply_message = "Hello from server!";

    
    server_fd = socket(AF_INET, SOCK_STREAM, 0);
    if (server_fd == -1) {
        printf("Socket creation failed\n");
        return -1;
    }

    
    server_addr.sin_family = AF_INET;
    server_addr.sin_addr.s_addr = INADDR_ANY; 
    server_addr.sin_port = htons(PORT);       

    
    if (bind(server_fd, (struct sockaddr *)&server_addr, sizeof(server_addr)) < 0) {
        printf("Bind failed\n");
        return -1;
    }


    if (listen(server_fd, 3) < 0) { 
        printf("Listen failed\n");
        return -1;
    }}
    printf("Server listening on port %d...\n", PORT);

    //
    int addrlen = sizeof(client_addr);
    client_fd = accept(server_fd, (struct sockaddr )&client_addr, (socklen_t)&addrlen);
    if (client_fd < 0) {
        printf("Accept failed\n");
        return -1;
    }
    printf("Connection accepted!\n");

     
    read(client_fd, buffer, 1024);
    printf("Client said: %s\n", buffer);

    
    write(client_fd, reply_message, strlen(reply_message));
    printf("Reply sent to client.\n");


    close(client_fd);
    close(server_fd);

    return 0;
}


// TCP CLIENT
#include <stdio.h>      
#include <string.h>     
#include <sys/socket.h> 
#include <arpa/inet.h>  
#include <unistd.h>    

#define PORT 8080 

int main() {
    int sock_fd;
    struct sockaddr_in server_addr;
    char buffer[1024] = {0};
    const char *client_message = "Hello from client!";


    if ((sock_fd = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
        printf("Socket creation error\n");
        return -1;
    }


    server_addr.sin_family = AF_INET;
    server_addr.sin_port = htons(PORT);
    
    server_addr.sin_addr.s_addr = inet_addr("127.0.0.1");

    
    if (connect(sock_fd, (struct sockaddr *)&server_addr, sizeof(server_addr)) < 0) {
        printf("Connection Failed\n");
        return -1;
    }
    printf("Connected to server!\n");

    
    write(sock_fd, client_message, strlen(client_message));
    printf("Message sent to server.\n");

    
    read(sock_fd, buffer, 1024);
    printf("Server replied: %s\n", buffer);

    
    close(sock_fd);

    return 0;
}

