'use strict';
var myurl;
console.log('Started', self);

self.addEventListener('install', function (event) {
    self.skipWaiting();
    console.log('Installed', event);
});

self.addEventListener('activate', function (event) {
    console.log('Activated', event);
});


self.addEventListener('push', function (event) {
    console.log('Push message', event);

    event.waitUntil(
    fetch('/notify.json').then(function (response) {
        return response.json().then(function (data) {
            console.log(JSON.stringify(data));
            var title = data.title;
            var body = data.body;
            myurl = data.myurl;

            return self.registration.showNotification(title, {
                body: body,
                icon: 'profile.png',
                tag: 'notificationTag'
            });

        });
    }).catch(function (err) {
        console.error('Unable to retrieve data', err);

        var title = 'An error occurred';
        var body = 'We were unable to get the information for this push message';

        return self.registration.showNotification(title, {
            body: body,
            icon: 'profile.png',
            tag: 'notificationTag'
        });
    })
    );
});

// var title = 'Vcona';
// event.waitUntil(
//   self.registration.showNotification(title, {
//     'body': 'School Management',
//     'icon': 'profile.png'
//   }));



self.addEventListener('notificationclick', function (event) {
    console.log('Notification click: tag', event.notification.tag);
    // Android doesn't close the notification when you click it
    // See http://crbug.com/463146
    event.notification.close();
    var url = 'https://demo.innotical.com';
    // Check if there's already a tab open with this URL.
    // If yes: focus on the tab.
    // If no: open a tab with the URL.
    event.waitUntil(
      clients.matchAll({
          type: 'window'
      })
      .then(function (windowClients) {
          console.log('WindowClients', windowClients);
          for (var i = 0; i < windowClients.length; i++) {
              var client = windowClients[i];
              console.log('WindowClient', client);
              if (client.url === url && 'focus' in client) {
                  return client.focus();
              }
          }
          if (clients.openWindow) {
              return clients.openWindow(myurl);
          }
      })
    );
});