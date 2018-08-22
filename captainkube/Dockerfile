# Build
FROM golang:1.10.3 as builder

WORKDIR /go/src/github.com/sabbour/phippy
COPY main.go .
RUN go get -d -v
RUN CGO_ENABLED=0 GOOS=linux go build -a -installsuffix cgo -o app .

# Run
FROM alpine:latest  

RUN apk --no-cache add ca-certificates
WORKDIR /root/
COPY --from=builder /go/src/github.com/sabbour/phippy/app .
CMD ["./app"]