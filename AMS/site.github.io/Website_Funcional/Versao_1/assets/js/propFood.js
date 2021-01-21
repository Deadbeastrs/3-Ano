var dict = {};

$( document ).ready(function() {
    localStorage.removeItem('Order')
    localStorage.removeItem('Rest')
});

function addFood(id){
    if(dict[id] == undefined){
        dict[id] = {
            'amount' : 1,
            'name' : document.getElementById("disp-"+id).parentElement.parentElement.getElementsByClassName("subttitle1")[0].innerHTML,
            'price' : document.getElementById("disp-"+id).parentElement.parentElement.getElementsByClassName("subttitle2")[0].innerHTML 
        }
    }
    else{
        dict[id]['amount'] += 1
    }
    updateFood(id)
}

function subFood(id){
    if(dict[id]['amount'] > 0){
        dict[id]['amount'] -= 1
    }
    if(dict[id]['amount'] == 0){
        delete dict[id]
    }
    updateFood(id)
}

function updateFood(id){
    if(dict[id] != undefined){
        document.getElementById("disp-"+id).innerHTML = dict[id]['amount'];
        if(dict[id]['amount'] != 0){
            document.getElementById("disp-"+id).parentElement.parentElement.parentElement.style.border = "solid 2px green"
        }else{
            document.getElementById("disp-"+id).parentElement.parentElement.parentElement.style.border = "solid 1px darkgray"
        }
    }else{
        document.getElementById("disp-"+id).innerHTML = 0;
        document.getElementById("disp-"+id).parentElement.parentElement.parentElement.style.border = "solid 1px darkgray"
    }
}

function confirm(){
    if(Object.keys(dict).length !== 0){
        localStorage.setItem('Order', JSON.stringify(dict));
        localStorage.setItem('Rest', document.getElementById('nome').innerHTML);
        window.location.href = "order.html";
    }else{
        alert("Tem que selecionar algo!");
    }
}
