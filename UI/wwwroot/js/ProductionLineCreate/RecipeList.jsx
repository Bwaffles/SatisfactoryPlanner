import React from 'react';
import makeDebugger from '../lib/makeDebugger.js';

const debug = makeDebugger('RecipeList');

export class RecipeList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            recipes: []
        };

        this.selectRecipe = this.selectRecipe.bind(this);
    }

    componentDidMount() {
        this.loadRecipesFromServer();
    }

    componentDidUpdate(prevProps) {
        if (this.props.itemId !== prevProps.itemId) {
            this.loadRecipesFromServer();
        }
    }

    loadRecipesFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', "recipes?itemId=" + this.props.itemId, true);
        xhr.onload = () => {
            const recipes = JSON.parse(xhr.responseText);
            this.setState({ recipes: recipes });
        };
        xhr.send();
    }

    selectRecipe(recipe) {
        debug("selectRecipe()");
        debug("recipe", recipe);

        this.props.onSelectRecipe(recipe);
    }

    render() {
        debug("render()");
        debug("props", this.props);
        debug("state", this.state);

        const { selectedRecipe } = this.props;
        const { recipes } = this.state;

        return (
            <div className="ui container recipe-list" style={{ marginTop: 3 + 'rem' }}>
                <h2 className="ui horizontal divider header">
                    Recipe
                </h2>
                {recipes.map((recipe) =>
                    <div className={"ui fluid raised card " + (selectedRecipe == recipe ? 'secondary' : '')} style={{ cursor: "pointer" }}
                        key={recipe.id} onClick={(e) => this.selectRecipe(recipe)}>
                        <div className="content">
                            <div className="header">{recipe.name}</div>
                            <div className="ui horizontal equal width segments">

                                <div className={"ui red segment left aligned " + (selectedRecipe == recipe ? 'secondary' : '')}>
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

                                <div className={"ui green segment left aligned " + (selectedRecipe == recipe ? 'secondary' : '')}>
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
