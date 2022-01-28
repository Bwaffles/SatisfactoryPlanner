import React from 'react';

export class RecipeList extends React.Component {
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
                {this.state.recipes.map((recipe) => <div className={"ui fluid raised card " + (this.props.selectedRecipe == recipe ? 'secondary' : '')} style={{ cursor: "pointer" }}
                    key={recipe.id} onClick={(e) => this.selectRecipe(recipe)}>
                    <div className="content">
                        <div className="header">{recipe.name}</div>
                        <div className="ui horizontal equal width segments">

                            <div className={"ui red segment left aligned " + (this.props.selectedRecipe == recipe ? 'secondary' : '')}>
                                <h3>
                                    <i className="right arrow red icon"></i>
                                    <span className="ui text black" style={{ marginLeft: 0.5 + 'rem' }}>
                                        Inputs
                                    </span>
                                </h3>
                                <div className="ui relaxed middle aligned list">

                                    {recipe.ingredients.map((ingredient) => <div key={ingredient.id} className="item">
                                        <div className="right floated content">
                                            <span className="ui grey text">{ingredient.itemsPerMinute}/min</span>
                                        </div>
                                        <div className="content">
                                            <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{ingredient.name}</span>
                                        </div>
                                    </div>
                                    )}

                                </div>
                            </div>

                            <div className={"ui green segment left aligned " + (this.props.selectedRecipe == recipe ? 'secondary' : '')}>
                                <h3>
                                    <span className="ui text black" style={{ marginRight: 0.5 + 'rem' }}>
                                        Outputs
                                    </span>
                                    <i className="right arrow green icon"></i>
                                </h3>

                                <div className="ui relaxed middle aligned list">

                                    {recipe.products.map((product) => <div key={product.id} className="item">
                                        <div className="right floated content">
                                            <span className="ui grey text">{product.itemsPerMinute}/min</span>
                                        </div>
                                        <div className="content">
                                            <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{product.name}</span>
                                        </div>
                                    </div>
                                    )}

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                )}
            </div>
        );
    }
}
