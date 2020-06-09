# Build
FROM golang:1.14.4 as builder

WORKDIR /build
COPY main.go .
# temporary workaroud for https://github.com/Azure/phippyandfriends/issues/44
RUN GO111MODULE=on; go mod init captainkube && go get k8s.io/client-go@v0.17.2 && go get -d -v
RUN CGO_ENABLED=0 GOOS=linux go build -a -installsuffix cgo -o app .

# Run
FROM alpine:latest  

RUN apk --no-cache add ca-certificates
WORKDIR /root/
COPY --from=builder /build/app .
CMD ["./app"]
