var addr = ''
var paym = ''
var resumo = ''
$( document ).ready(function() {
    var order = JSON.parse(localStorage.getItem('Order'));
    resumo = '<p class="text-black font-weight-bold">Resumo:</p>'
    var total = 0
    Object.keys(order).forEach(function(key) {
        resumo += '<p class="text-black itemsText"><span class="spanAmount">'+ order[key]['amount'] +'x&nbsp;</span>'+order[key]['name']+' - '+(parseInt(order[key]['amount']) * parseInt(order[key]['price'].split('€')[0]))+'€</p>'
        total += (parseInt(order[key]['amount']) * parseInt(order[key]['price'].split('€')[0]))
    });
    resumo += '<p class="text-black font-weight-light itemsText">Taxa de entrega - 1€</p>'
    resumo += '<p class="text-black font-weight-bold itemsText">Total: '+(total+1)+'€</p><hr>'
    document.getElementById('items').innerHTML = resumo;
    // Ir a base de dados buscar o primeiro cartao e o primeiro endereço e associar
});

function verifica() {
    addr = document.getElementById('endre').innerHTML.split('<i')[0]
    fulladdr = document.getElementById('endrInfo').getElementsByClassName('text-add')[0].innerHTML
    paym = document.getElementById('carta').innerHTML.split('<i')[0]
    tudo = resumo + '<p class="text-black font-weight-bold">Endereço:</p>' + '<p class="text-black itemsText">--'+addr+'--<br>' + fulladdr + '</p>'
    tudo += '<p class="text-black font-weight-bold">Pagamento:</p>' + '<p class="text-black itemsText">' + paym + '</p>'
    document.getElementById('finalItems').innerHTML = tudo;
}