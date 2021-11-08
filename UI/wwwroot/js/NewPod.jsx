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
            selectedItem: null,
            selectedRecipe: null
        };

        this.selectItem = this.selectItem.bind(this);
        this.selectRecipe = this.selectRecipe.bind(this);
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

    selectRecipe(recipe) {
        console.debug(recipe);
        this.setState({
            selectedRecipe: recipe
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
                    <RecipeList item={this.state.selectedItem} selectedRecipe={this.state.selectedRecipe} onSelectRecipe={this.selectRecipe} />
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

        this.selectRecipe = this.selectRecipe.bind(this);
    }

    selectRecipe(recipe) {
        this.props.onSelectRecipe(recipe);
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
                {this.state.recipes.map((recipe) =>
                    <div className={"ui segment " + (this.props.selectedRecipe == recipe ? 'secondary' : '')} style={{ cursor: "pointer" }}
                        key={recipe.id} onClick={(e) => this.selectRecipe(recipe)}>
                        <div className="header" style={{ marginBottom: 0.5 + 'rem' }}>{recipe.name}</div>
                        <div className="ui horizontal equal width segments">
                            <div className="ui red segment left aligned">
                                <h3>
                                    <i className="right arrow red icon"></i>
                                    <span className="ui text black" style={{ marginLeft: 0.5 + 'rem' }}>
                                        Inputs
                                            </span>
                                </h3>
                                {recipe.ingredients.map((ingredient) =>
                                    <p key={ingredient.id}>
                                        <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{ingredient.name}</span>
                                        <span className="ui grey text">{ingredient.itemsPerMinute}/min</span>
                                    </p>
                                )}
                            </div>
                            <div className="ui green segment right aligned">
                                <h3>
                                    <span className="ui text black" style={{ marginRight: 0.5 + 'rem' }}>
                                        Outputs
                                            </span>
                                    <i className="right arrow green icon"></i>
                                </h3>
                                {recipe.products.map((product) =>
                                    <p key={product.id}>
                                        <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{product.name}</span>
                                        <span className="ui grey text">{product.itemsPerMinute}/min</span>
                                    </p>
                                )}
                            </div>
                        </div>
                    </div>
                )}
            </div>
        );
    }
}

const element = <NewProductionLine url="/ProductionLine/Items" />;
ReactDOM.render(element, document.getElementById('newPod'));