const afToken = document.getElementById('RequestVerificationToken').value;

function apiRequest(url, data, onSuccess, onError) {
    return fetch(url, getFetchDefaults(JSON.stringify(data)))
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

function getFetchDefaults(data) {
    return {
        method: 'POST',
        credentials: 'same-origin',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': afToken,
        },
        body: data
    };
}

export default {
    apiRequest,
    getFetchDefaults
}