import { UserList } from "./components/UserList";
import { Card } from "antd";
import { FilterBlock } from "./components/FilterBlock";
import { useEffect } from "react";
import { useStore } from "./hooks/use-store";
import "./App.css";
import { SourceType } from "./helper";

const App = () => {
    const { taskStore } = useStore();

    useEffect(() => {
        taskStore.load();
    }, []);

    return (
        <>
            <Card
                title={"SIMPLE USER LIST APP"}
                className="main-card">
                <FilterBlock />
                <UserList />
            </Card>
        </>
    )
};

export default App;
