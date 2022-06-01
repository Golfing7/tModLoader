﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace tModPorter.Rewriters;

public static class SimpleSyntaxFactory
{
	public static SyntaxToken OperatorToken(SyntaxKind kind) =>
		Token(new(Space), kind, new(Space));

	public static MemberAccessExpressionSyntax SimpleMemberAccessExpression(ExpressionSyntax expression, string memberName) =>
		MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expression, IdentifierName(memberName));

	public static AssignmentExpressionSyntax SimpleAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right) =>
		AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, OperatorToken(SyntaxKind.EqualsToken), right);

	public static BinaryExpressionSyntax SimpleBinaryExpression(SyntaxKind kind, ExpressionSyntax left, ExpressionSyntax right) {
		var expr = BinaryExpression(kind, left, right);
		return expr.WithOperatorToken(expr.OperatorToken.WithLeadingTrivia(Space).WithTrailingTrivia(Space));
	}

	public static UsingDirectiveSyntax SimpleUsing(string ns) {
		var u = UsingDirective(IdentifierName(ns.ToString()));
		return u.WithUsingKeyword(u.UsingKeyword.WithTrailingTrivia(Space)).WithTrailingTrivia(CarriageReturnLineFeed);
	}

	public static ExpressionSyntax Parens(ExpressionSyntax expr) {
		// TODO, only wrap if necessary
		return ParenthesizedExpression(expr);
	}
}
