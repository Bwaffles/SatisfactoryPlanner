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
            selectedItem: e.target.value,
            selectedRecipe: null
        });

    }

    selectRecipe(recipe) {
        this.setState({
            selectedRecipe: recipe
        });
    }

    componentDidMount() {
        this.loadItemsFromServer();
    }

    render() {
        return (
            <div className="ui centered grid">
                <div className="eight wide column">
                    <div className="ui center aligned blue very padded text raised segment">
                        <h2 className="ui horizontal divider header">
                            Item
                        </h2>
                        <select className="ui fluid dropdown" onChange={(e) => this.selectItem(e)}>
                            {this.state.items.map((item) =>
                                <option key={item.code} value={item.code}>{item.name}</option>
                            )}
                        </select>
                        {this.state.selectedItem != null &&
                            <RecipeList item={this.state.selectedItem} selectedRecipe={this.state.selectedRecipe} onSelectRecipe={this.selectRecipe} />
                        }
                    </div>
                </div>
                {this.state.selectedRecipe != null &&
                    <Calculator item={this.state.selectedItem} recipe={this.state.selectedRecipe} />
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
            <div className="ui container recipe-list" style={{ marginTop: 3 + 'rem' }}>
                <h2 className="ui horizontal divider header">
                    Recipe
                </h2>
                {this.state.recipes.map((recipe) =>
                    <div className={"ui segment " + (this.props.selectedRecipe == recipe ? 'secondary' : '')} style={{ cursor: "pointer" }}
                        key={recipe.id} onClick={(e) => this.selectRecipe(recipe)}>
                        <h3>{recipe.name}</h3>
                        <div className="ui horizontal equal width segments">
                            <div className={"ui red segment left aligned " + (this.props.selectedRecipe == recipe ? 'secondary' : '')}>
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
                            <div className={"ui green segment left aligned " + (this.props.selectedRecipe == recipe ? 'secondary' : '')}>
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

class Calculator extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            totalItem: this.props.recipe
                .products
                .find(product => product.id == this.props.item)
                .itemsPerMinute
        };

        this.updateTotals = this.updateTotals.bind(this);
    }

    updateTotals(e) {
        this.setState({
            totalItem: e.target.value
        });
    }

    getRatio() {
        return this.state.totalItem / this.getOutputItem().itemsPerMinute;
    }

    getOutputItem() {
        return this.props.recipe.products
            .find(product => product.id == this.props.item);
    }

    render() {

        var recipe = this.props.recipe;
        var outputItem = this.getOutputItem();
        var productionRatio = this.getRatio();

        return (

            <div className="ui form eight wide column">
                <div className="ui center aligned blue very padded text raised segment">
                    <h2 className="ui horizontal divider header">
                        Calculator
                    </h2>

                    <p><b>Selected recipe:</b> {recipe.name}</p>

                    <div className="ui inline field">
                        <label>Target Output</label>
                        <div className="ui right labeled input">
                            <input type="text" value={this.state.totalItem} onChange={(e) => this.updateTotals(e)} />
                            <div className="ui label">
                                / min
                            </div>
                        </div>
                    </div>

                    <h3>Output</h3>
                    {recipe.products.map((product) =>
                        <p key={product.id}>
                            <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{product.name}</span>
                            <span className="ui grey text">{(product.itemsPerMinute * productionRatio)}/min</span>
                        </p>
                    )}

                    <h3>Input</h3>
                    {recipe.ingredients.map((ingredient) =>
                        <p key={ingredient.id}>
                            <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{ingredient.name}</span>
                            <span className="ui grey text">{(ingredient.itemsPerMinute * productionRatio)}/min</span>
                        </p>
                    )}

                    <h3>Machines</h3>
                    {this.state.totalItem / outputItem.itemsPerMinute}
                </div>
            </div>
        );
    }
}

const element = <NewProductionLine url="/ProductionLine/Items" />;
ReactDOM.render(element, document.getElementById('newPod'));