import React from 'react';


export class Calculator extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            totalItem: this.props.recipe
                .products
                .find(product => product.id == this.props.item)
                .itemsPerMinute,
            ingredients: this.props.recipe
                .ingredients
                .map(ingredient => {
                    return {
                        id: ingredient.id,
                        name: ingredient.name,
                        amount: ingredient.amount,
                        itemsPerMinute: ingredient.itemsPerMinute,
                        produceOnSite: false
                    };
                })
        };

        this.handleUpdateTotals = this.handleUpdateTotals.bind(this);
        this.handleProduceOnSite = this.handleProduceOnSite.bind(this);
    }

    handleUpdateTotals(e) {
        this.setState({
            totalItem: e.target.value
        });
    }

    handleProduceOnSite(event, ingredient) {
        console.debug(ingredient);
        console.debug(event);

        var produceOnSite = event.target.checked;

        this.setState(state => {
            const ingredients = this.state.ingredients.map(existingIngredient => {
                if (existingIngredient.id == ingredient.id) {
                    existingIngredient.produceOnSite = produceOnSite;
                }

                return existingIngredient;
            });

            return ingredients;
        });
    }

    componentDidUpdate(prevProps) {
        if (this.props.recipe !== prevProps.recipe) {

            this.setState({
                totalItem: this.props.recipe
                    .products
                    .find(product => product.id == this.props.item)
                    .itemsPerMinute,
                ingredients: this.props.recipe
                    .ingredients
                    .map(ingredient => {
                        return {
                            id: ingredient.id,
                            name: ingredient.name,
                            amount: ingredient.amount,
                            itemsPerMinute: ingredient.itemsPerMinute,
                            produceOnSite: false // TODO maybe persist if the same ingredient was present in last recipe
                        };
                    })
            });

        }
    }

    getRatio() {
        return this.state.totalItem / this.getOutputItem().itemsPerMinute;
    }

    getOutputItem() {
        return this.props.recipe.products
            .find(product => product.id == this.props.item);
    }

    getIngredientsProducedOnSite() {
        return this.state.ingredients.filter(ingredient => ingredient.produceOnSite);
    }

    render() {

        var recipe = this.props.recipe;
        var outputItem = this.getOutputItem();
        var productionRatio = this.getRatio();
        var ingredientsProducedOnSite = this.getIngredientsProducedOnSite();

        console.debug("props", this.props);
        console.debug("state", this.state);

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
                            <input
                                type="text"
                                value={this.state.totalItem}
                                onChange={(e) => this.handleUpdateTotals(e)} />
                            <div className="ui label">
                                / min
                            </div>
                        </div>
                    </div>

                    <h3>Output</h3>
                    {recipe.products.map((product) => <p key={product.id}>
                        <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{product.name}</span>
                        <span className="ui grey text">{(product.itemsPerMinute * productionRatio)}/min</span>
                    </p>
                    )}

                    <h3>Input</h3>
                    <div className="ui large relaxed middle aligned divided list">

                        {this.state.ingredients.map((ingredient) => <div key={ingredient.id} className="item">
                            <div className="right floated content">
                                <div className="ui toggle checkbox">
                                    <input
                                        type="checkbox"
                                        id={ingredient.id + "-produceOnSite"}
                                        checked={ingredient.produceOnSite}
                                        onChange={(e) => this.handleProduceOnSite(e, ingredient)} />
                                    <label htmlFor={ingredient.id + "-produceOnSite"}>Produce on site</label>
                                </div>
                            </div>
                            <div className="content">
                                <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{ingredient.name}</span>
                                <span className="ui grey text">{(ingredient.itemsPerMinute * productionRatio)}/min</span>
                            </div>
                        </div>
                        )}

                    </div>

                    <h3>Machines</h3>
                    {this.state.totalItem / outputItem.itemsPerMinute}
                </div>

                {ingredientsProducedOnSite.map(ingredient => <div key={ingredient.id} className="ui fluid raised card">
                    <div className="content">
                        <div className="header">
                            {ingredient.name}
                        </div>
                        Something is being produced on site!
                    </div>
                </div>
                )}
            </div>
        );
    }
}
