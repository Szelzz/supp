﻿const afToken = document.getElementById('RequestVerificationToken').value;

function apiRequest(url, data, onSuccess, onError) {
    return fetch(url, getFetchDefaults())
        .then(r => {
            if (r.ok)
                return r;
            throw Error(r);
        })
        .then(r => r.json())
        .then(json => {
            onSuccess(json);
        })
        .catch(e => {
            if (onError)
                onError(null);
        });
}

function getFetchDefaults() {
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

export default {
    apiRequest,
    getFetchDefaults
}