class NewProductionLine extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [
            ]
        };

        this.startCreatingPod = this.startCreatingPod.bind(this);
    }

    startCreatingPod() {
        // TODO update pod list to have a new PodItemCreator at the top of the list
        this.setState((state, props) => ({
            items: [...state.items, { id: 3 }]
        }));
    }

    render() {
        return (
            <div className="new-production-line">
                <div className="ui horizontal divider">
                    Pods
                </div>
                {this.state.items.map((item) =>
                    <PodItemCreator key={item.id} />
                )}
                <AddPodItem onAddNewPod={this.startCreatingPod} />
            </div>
        );
    }
}

// Prompt to start creating a new pod item
class AddPodItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isToggleOn: true
        };

        // This binding is necessary to make `this` work in the callback
        this.addPod = this.addPod.bind(this);
    }

    addPod() {
        this.props.onAddNewPod();
    }

    render() {
        return (
            <div className="ui center aligned blue very padded text raised container segment">
                <div className="ui icon header">
                    Add New Pod
                    </div>
                <div className="inline">
                    <button className="ui primary button" onClick={this.addPod}>
                        Add <i className="icon add"></i>
                    </button>
                </div>
            </div>
        );
    }
}

class PodItemCreator extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [],
            selectedItem: null
        };

        this.selectItem = this.selectItem.bind(this);
    }

    loadItemsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', "items", true);
        xhr.onload = () => {
            const items = JSON.parse(xhr.responseText);
            this.setState({ items: items });
        };
        xhr.send();
    }

    selectItem(e) {
        this.setState({
            selectedItem: e.target.value
        });
    }

    componentDidMount() {
        this.loadItemsFromServer();
    }

    render() {
        return (
            <div className="ui center aligned blue very padded text raised container segment">
                <div className="ui header">
                    Pick item and recipe
                </div>
                <select className="ui fluid dropdown" onChange={(e) => this.selectItem(e)}>
                    {this.state.items.map((item) =>
                        <option key={item.code} value={item.code}>{item.name}</option>
                    )}
                </select>
                <br />
                {this.state.selectedItem != null &&
                    <RecipeList item={this.state.selectedItem} />
                }

            </div>
        );
    }
}

class RecipeList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            recipes: []
        };
    }

    loadRecipesFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', "recipes?itemCode=" + this.props.item, true);
        xhr.onload = () => {
            const recipes = JSON.parse(xhr.responseText);
            this.setState({ recipes: recipes });
        };
        xhr.send();
    }

    componentDidUpdate(prevProps) {
        // Typical usage (don't forget to compare props):
        if (this.props.item !== prevProps.item) {
            this.loadRecipesFromServer();
        }
    }

    componentDidMount() {
        this.loadRecipesFromServer();
    }

    render() {
        return (
            <div className="ui container recipe-list">
                <h4 className="ui horizontal divider header">
                    Recipes
                </h4>
                <div className="ui one centered cards">
                    {this.state.recipes.map((recipe) =>
                        <a className="ui card" key={recipe.id}>
                            <div className="content">
                                {recipe.name}
                            </div>
                        </a>
                    )}
                </div>
            </div>
        );
    }
}

const element = <NewProductionLine url="/ProductionLine/Items" />;
ReactDOM.render(element, document.getElementById('newPod'));