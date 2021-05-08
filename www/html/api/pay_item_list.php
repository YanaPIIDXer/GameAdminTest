<?php
    $items = [];
    try
    {
        //                                               ↓良い子はマネしないでね
        $pdo = new PDO("mysql:dbname=test_game;host=db", "root", "secret");
        $stmt = $pdo->query("select * from pay_items;");
        while ($result = $stmt->fetch(PDO::FETCH_ASSOC))
        {
            array_push($items, ["id" => $result["id"], "name" => $result["name"], "price" => $result["price"]]);
        }
    }
    catch (PDOException $e)
    {
        print("PDOException! " . $e->getMessage());
        die();
    }
    echo json_encode($items);
?>