// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function addToCart(productId) {
    fetch("/Cart/AddToCart", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ productId: productId })
    })
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        })
        .catch(error => console.error(error));
}

function removeFromCart(productId) {
    fetch("/Cart/RemoveFromCart", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ productId: productId })
    })
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        })
        .catch(error => console.error(error));
}

function addQuaintity(productId, quaintity) {
    fetch("/Cart/AddQuaintity", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ productId: productId, quaintity: quaintity })
    })
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        })
        .catch(error => console.error(error));
}

function minusQuaintity(productId, quaintity) {
    fetch("/Cart/MinusQuaintity", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ productId: productId, quaintity: quaintity })
    })
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        })
        .catch(error => console.error(error));
}

function checkoutHandler() {
    fetch("/Cart/MinusQuaintity", {
        method: "POST"
    })
        .catch(error => console.error(error));
}