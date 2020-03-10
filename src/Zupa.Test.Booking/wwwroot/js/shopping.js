document.addEventListener("DOMContentLoaded", function () {
    fetch('/api/products')
        .then(function (response) {
            return response.json();
        })
        .then(function (products) {
            let productsList = document.getElementById('productList');

            return products.map(function (product) {
                let li = document.createElement('li');
                li.className = "list-group-item d-flex justify-content-between lh-condensed";
                li.onclick = function () {
                    addToBasket(product);
                };
                li.id = product.id;
                let div = document.createElement('div');
                let h6 = document.createElement('h6');
                h6.className = "my-0";
                h6.innerText = product.name;
                let small = document.createElement('small');
                small.className = "text-muted";
                small.innerText = "click to add to basket";
                let span = document.createElement('span');
                span.className = "text-muted";
                span.innerText = product.grossPrice;

                li.appendChild(div);
                div.appendChild(h6);
                div.appendChild(small);
                li.appendChild(span);

                productsList.appendChild(li);
            });
        });

    fetch('/api/baskets')
        .then(function (response) {
            return response.json();
        })
        .then(function (basket) {
            updateBasketView(basket);
            resetBasketCount(basket.items.length);
        });
});

function emptyBasketView() {
    var basketView = document.getElementById('currentBasket');
    while (basketView.lastElementChild) {
        basketView.removeChild(basketView.lastElementChild);
    }
}

function resetBasketCount(basketSize) {
    let basketCount = document.getElementById('basketCount');
    basketCount.innerText = basketSize;
}

function updateBasketView(basket)
{
    let basketList = document.getElementById('currentBasket');
    
    let totalLi = document.createElement('li');
    totalLi.className = "list-group-item d-flex justify-content-between";
    let totalSpan = document.createElement('span');
    totalSpan.innerText = "Total (GBP)";
    totalLi.appendChild(totalSpan);
    let totalStrong = document.createElement('strong');
    totalStrong.innerText = "£0.00";    
    totalLi.appendChild(totalStrong);
    basketList.appendChild(totalLi);

    return basket.items.map(function (item) {
        let li = document.createElement('li');
        li.className = "list-group-item d-flex justify-content-between lh-condensed";;
        let div = document.createElement('div');
        let h6 = document.createElement('h6');
        h6.className = "my-0";
        h6.innerText = item.name;
        let small = document.createElement('small');
        small.className = "text-muted";
        small.innerText = item.quantity;
        let span = document.createElement('span');
        span.className = "text-muted";
        span.innerText = item.grossPrice;
        //

        li.appendChild(div);
        div.appendChild(h6);
        div.appendChild(small);
        li.appendChild(span);

        basketList.appendChild(li);
    }
    
    );

    
}

function addToBasket(product) {
    fetch('api/baskets', {
        method: 'PUT',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    }).then(function (response) {
        return response.json();
    }).then(function (basket) {
        emptyBasketView();
        resetBasketCount(basket.items.length);
        updateBasketView(basket);
        updateBasketTotal(basket.items);
        sessionStorage.removeItem("actualtotal");

    });
}

function placeOrder() {
    fetch('/api/baskets')
        .then(function (response) {
            return response.json();
        })
        .then(function (basket) {
            fetch('api/orders', {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(basket)
            }).then(function (response) {
                return response.json();
            }).then(function (order) {
                alert('your order has been placed');
                resetBasketCount(0);
                emptyBasketView();
                removeCoupan();
            });
        });
}
function validateCoupanCode() {
    if (sessionStorage.getItem('actualtotal') == null) {
        fetch('api/Discount/' + document.getElementById("Promocode").value, {
            method: 'GET',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (response) {
            if (response.status == 200) {
                alert('Coupan Code applied to the cart');
                updateGrossTotal();
            }
            else {
                alert('Invalid Coupan code')
            }
        })
    }
    else { alert("Discount coupan is already applied"); }
}
function updateBasketTotal(items) {
    var sum = 0;
    for (var i = 0; i < items.length; i++) {
        sum = sum + items[i].grossPrice;
    }
    document.getElementById('currentBasket').childNodes[1].children[1].innerHTML = "$" + sum.toString();
}

function updateGrossTotal() {
    fetch('api/Discount/' + document.getElementById("Promocode").value
    ).then(function (response) {
        return response.json();
    }).then(function (discount) {
        let discountper = discount.discountPercentage;
        let currenttotal = document.getElementById('currentBasket').childNodes[1].children[1].innerHTML.replace('$', '');
        let newtotal = currenttotal * (1 - discountper / 100);
        sessionStorage.setItem('actualtotal', currenttotal);
        document.getElementById('currentBasket').childNodes[1].children[1].innerHTML = "$" + newtotal.toString();  
        let removepromo = document.createElement("btn");
        removepromo.style ="color:green; font-size: 20px;"
        removepromo.id = "RemoveCoupan";
        removepromo.innerHTML = "Remove coupan";
        removepromo.addEventListener("click", removeCoupan);
        document.getElementById('removecoupan').appendChild(removepromo);
    }
    )
}
function removeCoupan() {
    document.getElementById("Promocode").value = "";
    document.getElementById("removecoupan").removeChild(document.getElementById('RemoveCoupan'));
    document.getElementById('currentBasket').childNodes[1].children[1].innerHTML = "$" + sessionStorage.getItem("actualtotal");
    sessionStorage.removeItem("actualtotal");
}







