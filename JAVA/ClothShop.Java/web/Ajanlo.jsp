
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Csomagajánló</title>
    </head>
    <body>
        <h1>Adja meg az adatokat</h1>

        <form action="AjanloServlet">
            <table>
                <tr><td>Cloth</td><td><input type="text" name="Cloth" value="oltony(fekete)" /></td></tr>
                <tr><td>Vasarlonev</td><td><input type="text" name="vasarlonev" value="Feher Jeno" /></td></tr>
                <tr><td>Ar</td><td><input type="number" name="Ar" value="100000" /></td></tr>
            </table>
            <input type="submit" value="Elküldés"/>
        </form>
    </body>
</html>
