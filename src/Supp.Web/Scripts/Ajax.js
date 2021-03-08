﻿const afToken = document.getElementById('RequestVerificationToken').value;

export default {
    apiRequest(url, data, onSuccess, onError) {
        return fetch(url, {
            method: 'POST',
            credentials: 'same-origin',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': afToken,
            },
            body: JSON.stringify(data)
        })
            .then(r => r.json())
            .then(json => {
                if (json.succeeded) {
                    if (onSuccess)
                        onSuccess(json);
                } else {
                    if (onError)
                        onError(json);
                }
            })
            .catch(e => {
                if (onError)
                    onError(null);
            });
    },
    getFetchDefaults() {
        return {
            method: 'POST',
            credentials: 'same-origin',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': afToken,
            },
        };
    }
}