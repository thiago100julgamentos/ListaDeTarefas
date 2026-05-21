const IdUsuario = localStorage.getItem('IdUsuarioLogado');

fetch('https://localhost:7199/Usuario/' + IdUsuario)

    .then(response => {
        if (!response.ok) {
            throw new Error('Não foi possível carregar os dados do perfil.');
        }
        return response.json();
    })
    .then(dadosUsuario => {
        console.log("Dados recebidos do C#:", dadosUsuario);
        document.getElementById('perfilNome').innerText = dadosUsuario.nome || dadosUsuario.Nome;
        document.getElementById('perfilEmail').innerText = dadosUsuario.email || dadosUsuario.Email;
    });

    function logout() {
        fetch('https://localhost:7199/Usuario/logout', { credentials: 'include' })
            .then(response => {
                console.log(response);
                window.location.href = "index.html"
            })
    }
