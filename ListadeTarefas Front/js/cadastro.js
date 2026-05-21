const myForm = document.getElementById('cadastroUsuario');

myForm.addEventListener('submit', function (event) {
    event.preventDefault();

    fetch('https://localhost:7199/Usuario/cadastrar', {
        method: 'POST',
        credentials:'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nome: document.getElementById("nome").value,
            email: document.getElementById("email").value,
            senha: document.getElementById("senha").value
        }),
    }).then(response => response.json())
    .then(data => {
        document.getElementById("respostaUsuario").innerHTML ="<h4>Cliente cadastrado com sucesso! <br>"
        +"Seu ID gerado foi: "+data.id+"</h4>";        
    })
});

