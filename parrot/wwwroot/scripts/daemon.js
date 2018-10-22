const connection = new signalR.HubConnectionBuilder()
    .withUrl('/daemonhub', {
        logger: signalR.LogLevel.Verbose
    })
    .build();

connection.onclose(() => {
    console.log('closed');
    startConnection();
});

var viewModel = new PodCardsViewModel();

connection.on('clusterViewUpdated', (pods) => {
    viewModel.clear();
    for (let i = 0; i < pods.length; i++) {
        viewModel.add(pods[i]);
    }
});

function PodCardsViewModel() {
    var self = this;
    self.pods = ko.observableArray([]);
    self.add = function(pod) { self.pods.push(pod) };
    self.clear = function() { self.pods.removeAll() }
}

function startConnection() {
    $('#connectionStatus').text('Trying to connect...');
    connection
        .start()
        .then(() => {
            $('#connectionStatus').text('Connected');
            console.log('connection started');
            try { ko.applyBindings(viewModel); } catch (err) { console.error(err); }
        })
        .catch(console.error);
}

startConnection();
