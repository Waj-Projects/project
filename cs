// SIMPLIFIED TCP SERVER
#include <stdio.h>      // For printf()
#include <string.h>     // For memset()
#include <sys/socket.h> // For socket(), bind(), listen(), accept()
#include <netinet/in.h> // For sockaddr_in
#include <unistd.h>     // For read(), write(), close()

#define PORT 8080 // Define a port number

int main() {
    int server_fd, client_fd;
    struct sockaddr_in server_addr, client_addr;
    char buffer[1024] = {0};
    const char *reply_message = "Hello from server!";

    // 1. Create a socket (get a phone)
    server_fd = socket(AF_INET, SOCK_STREAM, 0);
    if (server_fd == -1) {
        printf("Socket creation failed\n");
        return -1;
    }

    // Prepare the server address structure
    server_addr.sin_family = AF_INET;
    server_addr.sin_addr.s_addr = INADDR_ANY; // Accept connections from any IP
    server_addr.sin_port = htons(PORT);       // Set the port number

    // 2. Bind the socket to the address and port (assign a phone number)
    if (bind(server_fd, (struct sockaddr *)&server_addr, sizeof(server_addr)) < 0) {
        printf("Bind failed\n");
        return -1;
    }

    // 3. Listen for incoming connections (turn the phone on)
    if (listen(server_fd, 3) < 0) { // The '3' is the max number of pending connections
        printf("Listen failed\n");
        return -1;
    }
    printf("Server listening on port %d...\n", PORT);

    // 4. Accept a connection (answer the call)
    int addrlen = sizeof(client_addr);
    client_fd = accept(server_fd, (struct sockaddr )&client_addr, (socklen_t)&addrlen);
    if (client_fd < 0) {
        printf("Accept failed\n");
        return -1;
    }
    printf("Connection accepted!\n");

    // 5. Read message from client (listen to what the client says)
    read(client_fd, buffer, 1024);
    printf("Client said: %s\n", buffer);

    // 6. Write a reply to the client (talk back)
    write(client_fd, reply_message, strlen(reply_message));
    printf("Reply sent to client.\n");

    // 7. Close the sockets (hang up)
    close(client_fd);
    close(server_fd);

    return 0;
}


// SIMPLIFIED TCP CLIENT
#include <stdio.h>      // For printf()
#include <string.h>     // For memset()
#include <sys/socket.h> // For socket(), connect()
#include <arpa/inet.h>  // For inet_addr()
#include <unistd.h>     // For read(), write(), close()

#define PORT 8080 // The same port as the server

int main() {
    int sock_fd;
    struct sockaddr_in server_addr;
    char buffer[1024] = {0};
    const char *client_message = "Hello from client!";

    // 1. Create a socket (get a phone)
    if ((sock_fd = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
        printf("Socket creation error\n");
        return -1;
    }

    // Prepare the server address structure
    server_addr.sin_family = AF_INET;
    server_addr.sin_port = htons(PORT);
    // Use "127.0.0.1" which is your own computer (localhost)
    server_addr.sin_addr.s_addr = inet_addr("127.0.0.1");

    // 2. Connect to the server (dial the number)
    if (connect(sock_fd, (struct sockaddr *)&server_addr, sizeof(server_addr)) < 0) {
        printf("Connection Failed\n");
        return -1;
    }
    printf("Connected to server!\n");

    // 3. Write a message to the server (talk)
    write(sock_fd, client_message, strlen(client_message));
    printf("Message sent to server.\n");

    // 4. Read the server's reply (listen)
    read(sock_fd, buffer, 1024);
    printf("Server replied: %s\n", buffer);

    // 5. Close the socket (hang up)
    close(sock_fd);

    return 0;
}

