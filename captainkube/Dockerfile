# Build
FROM golang:1.10.3 as builder

WORKDIR /go/src/github.com/sabbour/phippy
COPY main.go .
RUN go get -d -v
# temporary workaroud for https://github.com/Azure/phippyandfriends/issues/30
RUN cd $GOPATH/src/k8s.io/klog && git checkout v0.4.0 && cd -
RUN CGO_ENABLED=0 GOOS=linux go build -a -installsuffix cgo -o app .

# Run
FROM alpine:latest  

RUN apk --no-cache add ca-certificates
WORKDIR /root/
COPY --from=builder /go/src/github.com/sabbour/phippy/app .
CMD ["./app"]
