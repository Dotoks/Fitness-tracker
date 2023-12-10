function Header() {
    return (
        <header>
            <h1>Fitness Tracker</h1>
            <nav>
                <ul>
                    <li><a href='../Home/index.html'>Home</a></li>
                    <li><a href='../Recipes/recipes.html'>Recipes</a></li>
                    <li><a href='../Search/search.html'>Search</a></li>
                    <li><a href='../About/about.html'>About</a></li>
                    <li><a href='../Account/account.html'>Account</a></li>
                </ul>
            </nav>
            <div className="header-icons">
                <a href="#" className="icon-link"><img src="../Images/facebook-icon.png" alt="Facebook" style={{ width: '32px', height: '32x' }} /></a>
                <a href="#" className="icon-link"><img src="../Images/instagram-icon.png" alt="Instagram" style={{ width: '30px', height: '30px' }} /></a>
                <a href="#" className="icon-link"><img src="../Images/x-icon.png" alt="X" style={{ width: '30px', height: '30px' }} /></a>            </div>
        </header>
    );
}
