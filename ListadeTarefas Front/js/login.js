const myForm = document.getElementById('loginUsuario');

myForm.addEventListener('submit', function (event) {
    event.preventDefault();

    fetch('https://localhost:7199/Usuario/login', {
        method: 'POST',
        credentials:'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nome: " ",
            email: document.getElementById("email").value,
            senha: document.getElementById("senha").value
        }),
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Usuário ou senha incorretos');
        }
        return response.json();
    })
    .then(data => {
        document.getElementById("respostaUsuario").innerHTML ="<h4>Bem-vindo(a), "+ data.mensagem +"! Login realizado com sucesso.</h4>" 
        
    if (data.usuarioId) {
        localStorage.setItem('IdUsuarioLogado', data.usuarioId);  
    }
    setTimeout(function () {
        window.location.href = "home.html";
    }, 1500);
    })
    .catch(error => {
        document.getElementById("respostaUsuario").innerHTML = "<h4 style='color: red;'>Erro ao fazer login. Verifique seus dados.</h4>";
        console.error('Erro detectado:', error);
    });
});